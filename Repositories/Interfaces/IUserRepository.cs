using FraktonProject.Data;
using FraktonProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FraktonProject.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<RefreshToken> GetUserRefreshToken(int id, string token);
        Task<ApplicationUser> GetByIdAsync(int id);
    }
}
