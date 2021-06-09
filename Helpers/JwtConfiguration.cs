using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FraktonProject.Helpers
{
    public class JwtConfiguration
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string JwtExpiration { get; set; }
        public string RefreshTokenExpiration { get; set; }
    }
}
