using Domain.Guards;

namespace Domain.Entities;
public class UserEntity : BaseEntity
{
    public string? Email { get; private set; }
    public string? PasswordHash { get; private set; }

    public IEnumerable<RoleEntity>? Roles { get; private set; }
    public IEnumerable<RefreshTokenEntity>? RefreshTokens { get; private set; }

    private UserEntity() { }

    public static UserEntity Create(string email, string passwordHash)
    {
        UserGuards.EnsureEmailIsValid(email);

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash is required", nameof(passwordHash));

        return new UserEntity
        {
            Email = email,
            PasswordHash = passwordHash,
            Roles = new List<RoleEntity>(),
            RefreshTokens = new List<RefreshTokenEntity>()
        };
    }
}
