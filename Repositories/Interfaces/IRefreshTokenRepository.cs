using FraktonProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FraktonProject.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<IEnumerable<RefreshToken>> GetAllAsync();
        Task<bool> DeleteAsync(int id);
        Task<RefreshToken> AddAsync(RefreshToken entity);
        Task<RefreshToken> UpdateAsync(RefreshToken entity);
        Task<RefreshToken> GetByIdAsync(int id);
    }
}
