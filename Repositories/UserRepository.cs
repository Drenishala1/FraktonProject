using FraktonProject.Data;
using FraktonProject.Models;
using FraktonProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FraktonProject.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken> GetUserRefreshToken(int id, string token)
        {
            var userRefresh = await _context.Set<RefreshToken>()
                  .Where(x => x.BelongsToUserId == id && x.Token == token)
                  .FirstOrDefaultAsync();

            return userRefresh;
        }

        public async Task<ApplicationUser> GetByIdAsync(int id)
        {
            var result = await _context.Set<ApplicationUser>()
                               .Where(x => string.Equals(x.Id, id)).FirstOrDefaultAsync();

            return result;
        }
    }
}
