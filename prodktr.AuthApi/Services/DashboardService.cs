using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using prodktr.AuthApi.Data.Interfaces;
using prodktr.AuthApi.Services.Interfaces;

namespace prodktr.AuthApi.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUsersRepository _userRepo;
        private readonly IMapper _mapper;

        public DashboardService(IUsersRepository userRepo, IMapper mapper)
        {
            this._userRepo = userRepo;
            this._mapper = mapper;
        }

        public async Task<InstrumentConfuguredByYouDto> GetInstrumentconfig_Configby_You(string id)
        {
            var response = await _userRepo.GetInstrumentconfig_Configby_You(id);
            return response;
        }
        public async Task<InstrumentResponseDto> GetAllInstrumentConfigs()
        {
            var response = await _userRepo.GetAllInstrumentConfigs();
            return response;
        }

    }
}
