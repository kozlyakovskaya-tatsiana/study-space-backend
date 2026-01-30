namespace Domain.Entities;

public class RoleEntity : BaseEntity
{
    public string Name { get; private set; }
    public IEnumerable<UserEntity>? Users { get; private set; }

    private RoleEntity() { }

    public static RoleEntity Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Role name is required", nameof(name));

        return new RoleEntity
        {
            Name = name
        };
    }
}