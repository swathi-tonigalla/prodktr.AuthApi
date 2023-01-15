using Microsoft.IdentityModel.Tokens;
using prodktr.AuthApi.Data.Interfaces;
using prodktr.AuthApi.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using prodktr.AuthApi.Models;
using Microsoft.AspNetCore.Components;

namespace prodktr.AuthApi.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;

        }

       
        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(u => u.name.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }
       
        public async Task<User> GetUserByRefreshToken(string refreshToken)
        {
            var user = await _context.Users.FirstAsync(u => u.remember_token == refreshToken);
            return user;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.email == email);
                return user;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public async Task<User> GetUser(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.name == username);
            return user;
        }
        public async Task<bool> UpdateUser(User user)
        {
            var existingRecord = await _context.Users
               .FirstOrDefaultAsync(u => u.name.ToLower().Equals(user.name.ToLower()));
            if (user is null)
            {
                throw new Exception("User not found");
            }
            existingRecord.remember_token = user.remember_token;
            existingRecord.created_at = user.created_at;
            //existingRecord.TokenExpires = user.TokenExpires;

             _context.Update(existingRecord);
            await _context.SaveChangesAsync();
            return true;

        }
        public async Task<User> RegisterUser(User user)
        {     

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<Permission> GetPermissions(string? unique_id)
        {
            var user = await _context.Permission.FirstAsync(u => u.user_unique_id == unique_id);
            return user;
        }
    }
}
