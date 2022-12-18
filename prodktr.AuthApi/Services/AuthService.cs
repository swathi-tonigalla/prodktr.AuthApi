using Microsoft.EntityFrameworkCore;
using prodktr.AuthApi.Data.Interfaces;
using prodktr.AuthApi.Services.Interfaces;

namespace prodktr.AuthApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor,
            IAuthRepository authRepo)
        {
            this._httpContextAccessor = httpContextAccessor;
            _authRepo = authRepo;
        }

        public IAuthRepository _authRepo { get; }

        public async Task<AuthResponseDto> RefreshToken()
        {
            var refreshToken = _httpContextAccessor?.HttpContext?.Request.Cookies["refreshToken"];
            var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
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
            SetRefreshToken(newRefreshToken, user);

            return new AuthResponseDto
            {
                Success = true,
                Token = token,
                RefreshToken = newRefreshToken.Token,
                TokenExpires = newRefreshToken.Expires
            };
        }
    }
}
