namespace Domain.ValueObjects;

public sealed class RoleInfo(string name, string description)
{
    public string Name { get; } = name;
    public string Description { get; } = description;
}

