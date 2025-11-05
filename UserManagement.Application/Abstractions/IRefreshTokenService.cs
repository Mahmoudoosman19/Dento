using UserManagement.Domain.Entities;

namespace UserManagement.Application.Abstractions
{
    public interface IRefreshTokenService
    {
        Task<RefreshToken> RefreshToken(string token);
        Task RevokeToken(string token);
        RefreshToken GenerateRefreshToken(Guid userId);
        RefreshToken? GetRefreshToken(string token);
        Task SaveRefreshToken(RefreshToken refreshToken);
        Task DeleteUserRefreshTokens(Guid userId);
    }
}
