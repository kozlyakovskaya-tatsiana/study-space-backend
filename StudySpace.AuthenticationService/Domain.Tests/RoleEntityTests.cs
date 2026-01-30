using Domain.Entities;

namespace Domain.Tests
{
    public class RoleEntityTests
    {
        [Fact]
        public void Create_ValidName_ReturnsRoleEntityWithName()
        {
            const string name = "Admin";

            var role = RoleEntity.Create(name);

            Assert.NotNull(role);
            Assert.Equal(name, role.Name);
            Assert.Null(role.Users);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_InvalidName_ThrowsArgumentException(string invalidName)
        {
            var ex = Assert.Throws<ArgumentException>(() => RoleEntity.Create(invalidName));
        }
    }
}
