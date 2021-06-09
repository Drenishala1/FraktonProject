using FraktonProject.Data;
using FraktonProject.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FraktonProject.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticationResponseDTO> RefreshTokenAsync(RefreshTokenResponseDTO dto);
        Task<ApplicationUser> GetByIdAsync(int id);
    }
}
