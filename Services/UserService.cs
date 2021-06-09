using FraktonProject.Data;
using FraktonProject.Dtos;
using FraktonProject.Models;
using FraktonProject.Repositories.Interfaces;
using FraktonProject.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace FraktonProject.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJWTService jwtService;
        public UserService(IUserRepository userRepository, IJWTService jwtService, IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            this.jwtService = jwtService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<AuthenticationResponseDTO> RefreshTokenAsync(RefreshTokenResponseDTO dto)
        {
            AuthenticationResponseDTO response = null;
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var principals = handler.ReadToken(dto.JwtToken) as JwtSecurityToken;

                var userIdClaim = principals.Claims.Where(x => string.Equals(x.Type, "id", StringComparison.InvariantCultureIgnoreCase))
                                  .FirstOrDefault();

                if (!Int32.TryParse(userIdClaim.Value, out var userId))
                {
                    throw new SecurityTokenException();
                }

                //GetAll the user's refresh token
                var existingRefreshToken = await GetUserRefreshTokenAsync(userId, dto.RefreshToken);
                if (existingRefreshToken == null)
                {
                    throw new SecurityTokenException();
                }

                /*Check if the token has expired or it has been revoked
                 * If so, generate a new token and revoke the existing
                 * else return the existing one
                 * */
                var refreshToken = new RefreshToken();
                if (existingRefreshToken.ExpiresOn < DateTime.Now || existingRefreshToken.RevokedByUserId != null)
                {
                    refreshToken = jwtService.GenerateRefreshToken(userId);
                    await RevokeExpiredTokenAsync(userId, existingRefreshToken.Id, refreshToken);
                }
                else
                {
                    refreshToken = existingRefreshToken;
                }

                var user = await GetByIdAsync(userId);

                var token = jwtService.GenerateJwtToken(user);

                response = new AuthenticationResponseDTO()
                {
                    JwtToken = token,
                    RefreshToken = refreshToken.Token
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            return response;
        }

        public async Task<ApplicationUser> GetByIdAsync(int id)
        {
            try
            {
                var result = await _userRepository.GetByIdAsync(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private async Task<RefreshToken> GetUserRefreshTokenAsync(int id, string token)
        {
            try
            {
                var result = await _userRepository.GetUserRefreshToken(id, token);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private async Task RevokeExpiredTokenAsync(int userId, int tokenId, RefreshToken newToken)
        {
            try
            {
                var oldToken = await _refreshTokenRepository.GetByIdAsync(tokenId);

                oldToken.RevokedByUserId = userId;
                oldToken.RevokedOn = DateTime.Now;
                var user = await _userRepository.GetByIdAsync(userId);
                await _refreshTokenRepository.AddAsync(newToken);
                oldToken.RevokedByTokenId = newToken.Id;
                await _refreshTokenRepository.UpdateAsync(oldToken);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}
