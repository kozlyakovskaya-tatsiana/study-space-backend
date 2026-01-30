using Domain.Entities;

namespace Domain.Interfaces.Services;

public interface IAuthService
{
    Task<UserEntity> RegisterAsync(string email, string password, CancellationToken cancellationToken = default);
    Task<UserEntity> LoginAsync(string email, string password, CancellationToken cancellationToken = default);
    Task<RefreshTokenEntity> RefreshTokenAsync(string token, CancellationToken cancellationToken = default);
    Task RevokeRefreshTokenAsync(string token, CancellationToken cancellationToken = default);
}