using AutoMapper;
using prodktr.AuthApi.Data.Interfaces;
using prodktr.AuthApi.Services.Interfaces;

namespace prodktr.AuthApi.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUsersRepository _userRepo;
        private readonly IMapper _mapper;

        public UserService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor,
            IUsersRepository userRepo, IMapper mapper)
        {
            this._configuration = configuration;
            this._httpContextAccessor = httpContextAccessor;
            this._userRepo = userRepo;
            this._mapper = mapper;
        }

        public async Task<List<UserResponseDto>> GetAllUsers()
        {
            var users = await _userRepo.GetAllUsers();
            var dto = _mapper.Map<List<UserResponseDto>>(users);
            return dto;
        }
        public async Task<List<Client>> GetAllClients()
        {
            var users = await _userRepo.GetAllClients();
            return users;
        }
    }

    }
