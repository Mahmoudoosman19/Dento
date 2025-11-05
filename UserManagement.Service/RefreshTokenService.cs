using Common.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using UserManagement.Application.Abstractions;
using UserManagement.Application.Specifications.RefreshToken;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Services
{
    internal class RefreshTokenService : IRefreshTokenService
    {
        private readonly IGenericRepository<RefreshToken> _refreshTokenRepo;
        private readonly IConfiguration _configuration;
        private readonly IJwtProvider _jwtProvider;

        public RefreshTokenService(IGenericRepository<RefreshToken> refreshTokenRepo,
            IConfiguration configuration, IJwtProvider jwtProvider)
        {
            _refreshTokenRepo = refreshTokenRepo;
            _configuration = configuration;
            _jwtProvider = jwtProvider;
        }

        public async Task DeleteUserRefreshTokens(Guid userId)
        {
            (var refreshTokens, int count) = _refreshTokenRepo.GetWithSpec(new GetRefreshTokenByUserIdSpecification(userId));

            _refreshTokenRepo.DeleteRange(refreshTokens);

            await _refreshTokenRepo.SaveChangesAsync();
        }

        public RefreshToken GenerateRefreshToken(Guid userId)
        {
            var randomNumber = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            var token = Convert.ToBase64String(randomNumber);
            var expiresOn = DateTime.Now.AddDays(double.Parse(_configuration["RefreshToken:RefreshDurationInDays"]!));

            return new RefreshToken(token, expiresOn, userId);
        }

        public RefreshToken? GetRefreshToken(string token)
        {
            var refreshToken = _refreshTokenRepo.GetEntityWithSpec(new GetRefreshTokenWithUserSpecification(token));

            return refreshToken;
        }

        public async Task<RefreshToken> RefreshToken(string token)
        {
            var refreshToken = GetRefreshToken(token);

            if (refreshToken == null)
                throw new Exception("invalid refresh token");

            if (refreshToken.RevokedOn.HasValue)
                throw new Exception("inactive refresh token");

            if (!refreshToken.IsExpired)
            {
                var jwtToken = _jwtProvider.Generate(refreshToken.User);
                ///TODO NEED TO RETURN JWTTOKEN TO USER
                return refreshToken;
            }
            else
            {
                await RevokeToken(token);

                var newRefreshToken = GenerateRefreshToken(refreshToken.UserId);

                await SaveRefreshToken(refreshToken);

                return newRefreshToken;
            }
        }

        public async Task RevokeToken(string token)
        {
            var refreshToken = _refreshTokenRepo.GetEntityWithSpec(new GetRefreshTokenWithUserSpecification(token));

            if (refreshToken == null)
                return;

            refreshToken.Revoke();

            _refreshTokenRepo.Update(refreshToken);

            await _refreshTokenRepo.SaveChangesAsync();
        }

        public async Task SaveRefreshToken(RefreshToken refreshToken)
        {
            await _refreshTokenRepo.AddAsync(refreshToken);
            await _refreshTokenRepo.SaveChangesAsync();
        }
    }
}
