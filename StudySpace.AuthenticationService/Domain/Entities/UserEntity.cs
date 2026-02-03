using Domain.Enums;
using Domain.Guards;

namespace Domain.Entities;
public class UserEntity : BaseEntity
{
    public string? Email { get; private set; }
    public string? PasswordHash { get; private set; }

    public ICollection<UserRoleEntity>? Roles { get; private init; }
    public ICollection<RefreshTokenEntity>? RefreshTokens { get; private set; }

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
            Roles = new List<UserRoleEntity>(),
            RefreshTokens = new List<RefreshTokenEntity>()
        };
    }

    public void AddRole(RoleType role)
    {
        if (Roles!.Any(r => r.Role == role))
            return;

        Roles!.Add(UserRoleEntity.Create(this, role));
    }

    public bool HasRole(RoleType role)
    {
        return Roles!.Any(r => r.Role == role);
    }
}
