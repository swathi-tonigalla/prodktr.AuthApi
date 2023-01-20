namespace prodktr.AuthApi.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<InstrumentConfuguredByYouDto> GetInstrumentconfig_Configby_You(string id);
        Task<InstrumentResponseDto> GetAllInstrumentConfigs();
    }
}
