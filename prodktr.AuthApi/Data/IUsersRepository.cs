using Microsoft.EntityFrameworkCore;
using prodktr.AuthApi.Data.Interfaces;

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
        public async Task<List<User>> GetAllClients()
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
    }
}
