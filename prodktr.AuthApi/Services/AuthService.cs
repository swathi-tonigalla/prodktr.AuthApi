using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using prodktr.AuthApi.Data.Interfaces;
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
        public async Task<ServiceResponse<int>> RegisterUser(UserDto request)
        {
            var response = new ServiceResponse<int>();
            if (await _authRepo.UserExists(request.Username))
            {
                response.Success = false;
                response.Message = "User already exists.";
                return response;
            }
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Username = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

           var result = await _authRepo.RegisterUser(user);
            response.Data = result.Id;
            return response;
        }
        public async Task<ServiceResponse<AuthResponseDto>> Login(UserDto request)
        {
            var response = new ServiceResponse<AuthResponseDto>();
            var user = await _authRepo.GetUser(request.Username);
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
                       
            else if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password.";
            }
            else
            {
                string token = CreateToken(user);
                var refreshToken = CreateRefreshToken();
               await SetRefreshToken(refreshToken, user);

                var authDto =  new AuthResponseDto
                {
                    Success = true,
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
                return new AuthResponseDto { Message = "Invalid Refresh Token" };
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return new AuthResponseDto { Message = "Token expired." };
            }

            string token = CreateToken(user);
            var newRefreshToken = CreateRefreshToken();
             await SetRefreshToken(newRefreshToken, user);

            return new AuthResponseDto
            {
                Success = true,
                Token = token,
                RefreshToken = newRefreshToken.Token,
                TokenExpires = newRefreshToken.Expires
            };
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
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

            user.RefreshToken = refreshToken.Token;
            user.TokenCreated = refreshToken.Created;
            user.TokenExpires = refreshToken.Expires;

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
