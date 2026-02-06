using Domain.Enums;
using Domain.Exceptions;
using Domain.Guards;

namespace Domain.Entities;
public class UserEntity : BaseEntity
{
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }

    private List<UserRoleEntity> _roles = new ();
    public IReadOnlyCollection<UserRoleEntity> UserRoles => _roles.AsReadOnly();

    private List<RefreshTokenEntity> _refreshTokens = new ();
    public IReadOnlyCollection<RefreshTokenEntity> RefreshTokens => _refreshTokens.AsReadOnly();

    private UserEntity() { }

    public static UserEntity Create(
        string email,
        string passwordHash,
        IEnumerable<RoleType> roles)
    {
        UserGuards.EnsureEmailIsValid(email);

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash is required", nameof(passwordHash));

        ArgumentNullException.ThrowIfNull(roles);

        var roleList = roles.Distinct().ToList();
        if (roleList.Count == 0)
            throw new DomainException("User must have at least one role");

        var user = new UserEntity
        {
            Email = email,
            PasswordHash = passwordHash,
            _roles = [],
            _refreshTokens = []
        };

        foreach (var role in roleList)
        {
            user.AddRoleInternal(role);
        }

        return user;
    }

    public void AddRole(RoleType role)
    {
        if (_roles.Any(r => r.Role == role))
            return;

        _roles.Add(UserRoleEntity.Create(this, role));
    }

    public void RemoveRole(RoleType role)
    {
        var roleEntity = _roles.FirstOrDefault(r => r.Role == role);
        if (roleEntity is null)
            return;

        if (_roles.Count == 1)
            throw new DomainException("User must have at least one role");

        _roles.Remove(roleEntity);
    }

    public bool HasRole(RoleType role)
    {
        return _roles.Any(r => r.Role == role);
    }

    public void ChangePassword(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new ArgumentException("Password hash is required", nameof(newPasswordHash));

        PasswordHash = newPasswordHash;
    }

    private void AddRoleInternal(RoleType role)
    {
        _roles.Add(UserRoleEntity.Create(this, role));
    }
}