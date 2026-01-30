using Domain.Entities;

namespace Domain.Tests;

public class UserEntityTests
{
    [Fact]
    public void Create_ValidEmailAndPasswordHash_ReturnsUserEntity()
    {
        const string email = "user@example.com";
        const string passwordHash = "hashedpassword";

        var user = UserEntity.Create(email, passwordHash);

        Assert.Equal(email, user.Email);
        Assert.Equal(passwordHash, user.PasswordHash);
        Assert.NotNull(user.Roles);
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

