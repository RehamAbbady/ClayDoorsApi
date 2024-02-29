using DoorManagementSystem.Application.DTOs;
using DoorManagementSystem.Application.Services;
using Microsoft.Extensions.Configuration;
using Moq;

namespace DoorManagementSystem.Test.Services
{
    public class TokenServiceTests
    {
        private readonly Mock<IConfiguration> _configurationMock = new();
        private readonly Mock<IConfigurationSection> _configurationSectionMock = new();

        [Fact]
        public void GenerateJwtToken_ValidUser_ReturnsToken()
        {
            // Arrange
            var user = new UserDto { UserId = 1 };
            var key = "This is a very secret  long long long key";
            var issuer = "TestIssuer";
            var audience = "TestAudience";

            _configurationSectionMock.SetupGet(m => m[It.Is<string>(s => s == "Key")]).Returns(key);
            _configurationSectionMock.SetupGet(m => m[It.Is<string>(s => s == "Issuer")]).Returns(issuer);
            _configurationSectionMock.SetupGet(m => m[It.Is<string>(s => s == "Audience")]).Returns(audience);
            _configurationMock.Setup(a => a.GetSection("Jwt")).Returns(_configurationSectionMock.Object);

            var service = new TokenService(_configurationMock.Object);

            // Act
            var token = service.GenerateJwtToken(user);

            // Assert
            Assert.NotNull(token);
        }
    }

}
