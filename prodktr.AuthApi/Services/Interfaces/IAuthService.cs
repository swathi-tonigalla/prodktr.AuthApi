global using prodktr.AuthApi.Models;

namespace prodktr.AuthApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RefreshToken();
        Task<ServiceResponse<long>> RegisterUser(UserDto request);
        Task<ServiceResponse<AuthResponseDto>> Login(UserDto request);
        Task<AuthResponseDto> Authenticate(UserDto request);
    }
    public interface IDashboardService
    {

    }
    public interface IUserService
    {

    }
    public interface IClientService
    {

    }
}
