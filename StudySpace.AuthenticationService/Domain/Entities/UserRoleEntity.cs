using Domain.Enums;

namespace Domain.Entities;

public class UserRoleEntity : BaseEntity
{
    public Guid UserId { get; private set; }
    public UserEntity User { get; private set; } = null!;

    public RoleType Role { get; private set; }

    private UserRoleEntity() { }

    internal static UserRoleEntity Create(UserEntity? user, RoleType role)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        return new UserRoleEntity
        {
            User = user,
            UserId = user.Id,
            Role = role
        };
    }
}
