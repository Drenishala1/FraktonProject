using FraktonProject.Data;
using FraktonProject.Helpers;
using FraktonProject.Models;
using FraktonProject.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FraktonProject.Services
{
    public class JWTService : IJWTService
    {
        private readonly JwtConfiguration _jwtConfiguration;

        public JWTService(
            IOptions<JwtConfiguration> jwtConfiguration)
        {
            _jwtConfiguration = jwtConfiguration.Value;
        }

        public RefreshToken GenerateRefreshToken(int userId)
        {
            try
            {
                using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
                {
                    var randomBytes = new byte[64];
                    rngCryptoServiceProvider.GetBytes(randomBytes);
                    return new RefreshToken
                    {
                        Token = Convert.ToBase64String(randomBytes),
                        ExpiresOn = DateTime.Now.AddMinutes(int.Parse(_jwtConfiguration.RefreshTokenExpiration)),
                        BelongsToUserId = userId
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public string GenerateJwtToken(ApplicationUser user)
        {
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Secret));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>();


                var token = JwtSecurityToken(claims.ToArray(), credentials);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private JwtSecurityToken JwtSecurityToken(Claim[] claims, SigningCredentials credentials)
        {
            try
            {
                var token = new JwtSecurityToken(
                       _jwtConfiguration.Issuer,
                       _jwtConfiguration.Audience,
                       claims,
                       expires: DateTime.Now.AddMinutes(int.Parse(_jwtConfiguration.JwtExpiration)),
                       signingCredentials: credentials);
                return token;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}
