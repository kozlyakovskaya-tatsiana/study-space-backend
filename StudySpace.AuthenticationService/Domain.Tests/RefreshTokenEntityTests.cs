using Domain.Entities;

namespace Domain.Tests;

public class RefreshTokenEntityTests
{
    private const string Token = "token123";
    private readonly UserEntity _user = UserEntity.Create("User@example.com", "hash");

    [Fact]
    public void Create_ValidArguments_ReturnsRefreshTokenEntity()
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(10);

        var refreshToken = RefreshTokenEntity.Create(_user, Token, expiresAt);

        Assert.Equal(Token, refreshToken.Token);
        Assert.Equal(expiresAt, refreshToken.ExpiresAt);
        Assert.False(refreshToken.IsRevoked);
        Assert.Equal(_user.Id, refreshToken.UserId);
        Assert.Equal(_user, refreshToken.User);
    }

    [Fact]
    public void Create_NullUser_ThrowsArgumentNullException()
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(10);

        Assert.Throws<ArgumentNullException>(() => RefreshTokenEntity.Create(null, Token, expiresAt));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Create_InvalidToken_ThrowsArgumentException(string invalidToken)
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(10);

        Assert.Throws<ArgumentException>(() => RefreshTokenEntity.Create(_user, invalidToken, expiresAt));
    }

    [Fact]
    public void Create_ExpiredDate_ThrowsArgumentException()
    {
        var expiresAt = DateTime.UtcNow.AddSeconds(-1);

        Assert.Throws<ArgumentException>(() => RefreshTokenEntity.Create(_user, Token, expiresAt));
    }

    [Fact]
    public void Revoke_SetsIsRevokedTrue()
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(10);
        var refreshToken = RefreshTokenEntity.Create(_user, Token, expiresAt);

        refreshToken.Revoke();

        Assert.True(refreshToken.IsRevoked);
    }
}
