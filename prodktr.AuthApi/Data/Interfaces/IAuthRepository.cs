using prodktr.AuthApi.Services;

namespace prodktr.AuthApi.Data.Interfaces
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExists(string username);
        Task<User> GetUser(string refreshToken);
        Task<bool> UpdateUser(User user);
    }
}
