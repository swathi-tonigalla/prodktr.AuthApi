global using prodktr.AuthApi.Models;

namespace prodktr.AuthApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RefreshToken();
    }
}
