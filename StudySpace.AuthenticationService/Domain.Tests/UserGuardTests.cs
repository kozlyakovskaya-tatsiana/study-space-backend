using Domain.Guards;

namespace Domain.Tests;

public class UserGuardTests
{
    [Fact]
    public void EnsureEmailIsValid_ValidEmail_DoesNotThrow()
    {
        const string validEmail = "test@example.com";

        UserGuards.EnsureEmailIsValid(validEmail);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    [InlineData("invalid-email")]
    [InlineData("missing@domain")]
    public void EnsureEmailIsValid_InvalidEmail_ThrowsArgumentException(string invalidEmail)
    {
        Assert.Throws<ArgumentException>(() => UserGuards.EnsureEmailIsValid(invalidEmail));
    }
}