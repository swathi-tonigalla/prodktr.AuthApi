using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using prodktr.AuthApi.Data.Interfaces;
using prodktr.AuthApi.Models;
using prodktr.AuthApi.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace prodktr.AuthApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor,
            IAuthRepository authRepo)
        {
            this._configuration = configuration;
            this._httpContextAccessor = httpContextAccessor;
            _authRepo = authRepo;
        }

        public IAuthRepository _authRepo { get; }
        public async Task<ServiceResponse<long>> RegisterUser(UserDto request)
        {
            var response = new ServiceResponse<long>();
            if (await _authRepo.UserExists(request.email))
            {
                response.Success = false;
                response.Message = "User already exists.";
                return response;
            }
            CreatePasswordHash(request.email, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                name = request.email,
                //password = passwordHash,
                //PasswordSalt = passwordSalt
            };

           var result = await _authRepo.RegisterUser(user);
            response.Data = result.id;
            return response;
        }
        public async Task<AuthResponseDto> Authenticate(UserDto request)
        {
            var user = await _authRepo.GetUserByEmail(request.email);
            if(user==null)
            {
                throw new Exception("invalid_credentials");
            }
            var permissions = await _authRepo.GetPermissions(user.unique_id);
            string token = CreateToken(user);
            var authDto = new AuthResponseDto
            {
                Token = token,
                unique_id= user.unique_id,
                name= user.name,
                roles= user.primary_role,
                permission= permissions

            };
            return authDto;
        }

        public async Task<ServiceResponse<AuthResponseDto>> Login(UserDto request)
        {
            var response = new ServiceResponse<AuthResponseDto>();
            var user = await _authRepo.GetUser(request.email);
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
                       
            //else if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            //{
            //    response.Success = false;
            //    response.Message = "Wrong password.";
            //}
            else
            {
                string token = CreateToken(user);
                var refreshToken = CreateRefreshToken();
               await SetRefreshToken(refreshToken, user);

                var authDto =  new AuthResponseDto
                {
                    //Success = true,
                    Token = token,
                    RefreshToken = refreshToken.Token,
                    TokenExpires = refreshToken.Expires
                };
                response.Data = authDto;
            }

            return response;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<AuthResponseDto> RefreshToken()
        {
            var refreshToken = _httpContextAccessor?.HttpContext?.Request.Cookies["refreshToken"];
            var user = await _authRepo.GetUserByRefreshToken(refreshToken);
            if (user == null)
            {
                throw new Exception("Invalid Refresh Token");
            }
            
            //else if (user.TokenExpires < DateTime.Now)
            //{
            //    return new AuthResponseDto { Message = "Token expired." };
            //}

            string token = CreateToken(user);
            var newRefreshToken = CreateRefreshToken();
             await SetRefreshToken(newRefreshToken, user);

            return new AuthResponseDto
            {
                //Success = true,
                Token = token,
                RefreshToken = newRefreshToken.Token,
                TokenExpires = newRefreshToken.Expires
            };
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Name, user.name),
                new Claim(ClaimTypes.Role, user.primary_role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private RefreshToken CreateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }
        private async Task<bool> SetRefreshToken(RefreshToken refreshToken, User user)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expires,
            };
            _httpContextAccessor?.HttpContext?.Response
                .Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

            user.remember_token = refreshToken.Token;
            user.created_at = refreshToken.Created;
           // user.TokenExpires = refreshToken.Expires;

          return  await _authRepo.UpdateUser(user);
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
