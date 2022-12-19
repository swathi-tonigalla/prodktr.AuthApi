using Microsoft.IdentityModel.Tokens;
using prodktr.AuthApi.Data.Interfaces;
using prodktr.AuthApi.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Azure;
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
            if (await _context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }
       
        public async Task<User> GetUserByRefreshToken(string refreshToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            return user;
        }
        public async Task<User> GetUser(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            return user;
        }
        public async Task<bool> UpdateUser(User user)
        {
            var existingRecord = await _context.Users
               .FirstOrDefaultAsync(u => u.Username.ToLower().Equals(user.Username.ToLower()));
            if (user is null)
            {
                throw new Exception("User not found");
            }
            existingRecord.RefreshToken = user.RefreshToken;
            existingRecord.TokenCreated = user.TokenCreated;
            existingRecord.TokenExpires = user.TokenExpires;

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
    }
}
