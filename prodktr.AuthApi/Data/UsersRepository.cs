using Microsoft.EntityFrameworkCore;
using prodktr.AuthApi.Data.Interfaces;
using Ubiety.Dns.Core;

namespace prodktr.AuthApi.Data
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public UsersRepository(DataContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;

        }
        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                var user = await _context.Users.Where(x => x.is_deleted == 0).ToListAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
        public async Task<List<Client>> GetAllClients()
        {
            try
            {
                var user = await _context.Clients.Where(x => x.is_deleted == 0).ToListAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<InstrumentConfuguredByYouDto> GetInstrumentconfig_Configby_You(string uniqueId)
        {
            var response = new InstrumentConfuguredByYouDto()
            {
                instrumentConfuguredByYou = 0
            };
            try
            {
                var configList = await _context.Client_Project.Where(x => x.unique_id.Equals(uniqueId) && x.mappedInstrumentCount!=null).ToListAsync();
                if (configList!=null)
                {
                    response.instrumentConfuguredByYou = configList.Count;
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<InstrumentResponseDto> GetAllInstrumentConfigs()
        {
            var response = new InstrumentResponseDto()
            {
                AllUserInstrumentConfigured = 0
            };
            try
            {
                var configList = await _context.Client_Project.Where(x => x.mappedInstrumentCount != null).ToListAsync();
                if (configList != null)
                {
                    response.AllUserInstrumentConfigured = configList.Count;
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserPermissions> GetNetUserPermission(string uniqueId)
        {
            var response = new UserPermissions();
            try
            {
                var permissionsList = await _context.Permission.Where(x => x.user_unique_id.Equals(uniqueId)).FirstOrDefaultAsync();
                if (permissionsList != null)
                {
                    response.permission = permissionsList;
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
