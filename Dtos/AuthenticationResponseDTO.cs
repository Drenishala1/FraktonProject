using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FraktonProject.Dtos
{
    public class AuthenticationResponseDTO
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
