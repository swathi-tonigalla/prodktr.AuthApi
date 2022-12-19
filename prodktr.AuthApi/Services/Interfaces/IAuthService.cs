global using prodktr.AuthApi.Models;

namespace prodktr.AuthApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RefreshToken();
        Task<ServiceResponse<int>> RegisterUser(UserDto request);
        Task<ServiceResponse<AuthResponseDto>> Login(UserDto request);
    }
}
