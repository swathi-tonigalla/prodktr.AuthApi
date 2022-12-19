using prodktr.AuthApi.Services;

namespace prodktr.AuthApi.Data.Interfaces
{
    public interface IAuthRepository
    {
        Task<bool> UserExists(string username);
        Task<User> GetUserByRefreshToken(string refreshToken);
        Task<bool> UpdateUser(User user);
        Task<User> RegisterUser(User user);
        Task<User> GetUser(string username);
    }
}
