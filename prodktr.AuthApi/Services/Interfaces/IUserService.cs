namespace prodktr.AuthApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> GetAllUsers();
        Task<List<Client>> GetAllClients();
    }
}
