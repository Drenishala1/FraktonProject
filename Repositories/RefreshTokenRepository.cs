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
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext context;
        public RefreshTokenRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<RefreshToken> AddAsync(RefreshToken entity)
        {
            var result = new RefreshToken();
            await context.AddAsync(entity);
            await SaveChangesAsync();
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = new bool();
            var entity = await GetByIdAsync(id);
            context.Remove(entity);
            await SaveChangesAsync();
            return result;
        }

        public async Task<IEnumerable<RefreshToken>> GetAllAsync()
        {
            var result = await context.Set<RefreshToken>().ToListAsync();
            return result;
        }

        public async Task<RefreshToken> GetByIdAsync(int id)
        {
            var result = await context.Set<RefreshToken>().FindAsync(id);
            return result;
        }

        public async Task<RefreshToken> UpdateAsync(RefreshToken entity)
        {
            var result = new RefreshToken();
            context.Update(entity);
            await SaveChangesAsync();
            return result;
        }

        internal async Task<bool> SaveChangesAsync()
        {
            var result = await context.SaveChangesAsync();
            return result > 0;
        }
    }
}
