using Domain.ValueObjects;

namespace Domain.Enums;

public static class RoleTypeMetadata
{
    private static readonly Dictionary<RoleType, RoleInfo> _roles = new()
        {
            {
                RoleType.Admin,
                new RoleInfo ("Admin", "Platform administrator with full access")
            },
            {
                RoleType.Student, new RoleInfo ("Student", "Student of the educational platform")
            },
        };

    public static RoleInfo Get(RoleType role)
    {
        if (!_roles.TryGetValue(role, out var info))
            throw new ArgumentOutOfRangeException(nameof(role), role, "Unknown role");

        return info;
    }
}

