using Domain.Entities;

namespace Domain.Tests;

public class UserEntityTests
{
    private const string Email = "user@example.com";
    private const string PasswordHash = "hashedpassword";

    [Fact]
    public void Create_ValidEmailAndPasswordHash_ReturnsUserEntity()
    {
        var user = UserEntity.Create(Email, PasswordHash);

        Assert.Equal(Email, user.Email);
        Assert.Equal(PasswordHash, user.PasswordHash);
        Assert.NotNull(user.UserRoles);
        Assert.NotNull(user.RefreshTokens);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Create_InvalidPasswordHash_ThrowsArgumentException(string invalidPasswordHash)
    {
        const string email = "user@example.com";

        Assert.Throws<ArgumentException>(() => UserEntity.Create(email, invalidPasswordHash));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    [InlineData("invalid-email")]
    public void Create_InvalidEmail_ThrowsArgumentException(string invalidEmail)
    {
        const string passwordHash = "hashedpassword";

        Assert.Throws<ArgumentException>(() => UserEntity.Create(invalidEmail, passwordHash));
    }
}

