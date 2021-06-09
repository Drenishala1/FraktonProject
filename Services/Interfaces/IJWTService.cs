using FraktonProject.Data;
using FraktonProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FraktonProject.Services.Interfaces
{
    public interface IJWTService
    {
        RefreshToken GenerateRefreshToken(int userId);
        string GenerateJwtToken(ApplicationUser user);
    }
}
