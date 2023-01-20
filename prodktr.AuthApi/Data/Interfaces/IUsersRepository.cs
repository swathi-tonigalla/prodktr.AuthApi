namespace prodktr.AuthApi.Data.Interfaces
{
    public interface IUsersRepository
    {
        Task<List<User>> GetAllUsers();
        Task<List<Client>> GetAllClients();
        Task<InstrumentConfuguredByYouDto> GetInstrumentconfig_Configby_You(string uniqueId);
        Task<InstrumentResponseDto> GetAllInstrumentConfigs();
    }
}
