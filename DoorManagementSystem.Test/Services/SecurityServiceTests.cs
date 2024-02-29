using Xunit;
using Moq;
using DoorManagementSystem.Application.Services;
using DoorManagementSystem.Application.Interfaces.IServices;
using Microsoft.Extensions.Configuration;
using DoorManagementSystem.Application.DTOs;
using System.Text;

namespace DoorManagementSystem.Test
{
    public class SecurityServiceTests
    {
        private readonly ISecurityService _securityService = new SecurityService();

        [Fact]
        public void HashPin_ValidPin_ReturnsHashedPin()
        {
            // Arrange
            var pin = "1234";

            // Act
            var hashedPin = _securityService.HashPin(pin);

            // Assert
            Assert.NotNull(hashedPin);
            Assert.NotEqual(pin, hashedPin);
        }

        [Fact]
        public void VerifyPin_ValidPin_ReturnsTrue()
        {
            // Arrange
            var pin = "1234";
            var hashedPin = _securityService.HashPin(pin);

            // Act
            var isValid = _securityService.VerifyPin(pin, hashedPin);

            // Assert
            Assert.True(isValid);
        }
    }

}
