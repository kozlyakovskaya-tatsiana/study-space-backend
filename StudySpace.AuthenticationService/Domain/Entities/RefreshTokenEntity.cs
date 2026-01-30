namespace Domain.Entities;

public class RefreshTokenEntity : BaseEntity
{
    public string? Token { get; private set; }
    public DateTimeOffset ExpiresAt { get; private set; }
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    public bool IsRevoked { get; private set; }

    public Guid UserId { get; private set; }
    public UserEntity? User { get; private set; }

    private RefreshTokenEntity() { }

    public static RefreshTokenEntity Create(UserEntity? user, string token, DateTime expiresAt)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Token is required", nameof(token));
        if (expiresAt <= DateTime.UtcNow)
            throw new ArgumentException("Expiration must be in the future", nameof(expiresAt));

        return new RefreshTokenEntity
        {
            User = user,
            UserId = user.Id,
            Token = token,
            ExpiresAt = expiresAt,
            IsRevoked = false
        };

    }
    public void Revoke()
    {
        IsRevoked = true;
    }
}