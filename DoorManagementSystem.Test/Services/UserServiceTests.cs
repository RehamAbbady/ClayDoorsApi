using AutoMapper;
using DoorManagementSystem.Application.DTOs;
using DoorManagementSystem.Application.Interfaces.IRepositories;
using DoorManagementSystem.Application.Services;
using DoorManagementSystem.Domain.Entities;
using Moq;

namespace DoorManagementSystem.Test.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUsersRepository> _usersRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        [Fact]
        public async Task GetUserByIdAsync_ValidId_ReturnsUserDto()
        {
            // Arrange
            var userId = 1;
            var user = new User { UserId = userId, Email = "test@test.com" };
            var userDto = new UserDto { UserId = userId, Email = "test@test.com" };

            _usersRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);
            _mapperMock.Setup(mapper => mapper.Map<UserDto>(user)).Returns(userDto);

            var service = new UserService(_usersRepositoryMock.Object, _mapperMock.Object);

            // Act
            var result = await service.GetUserByIdAsync(userId);

            // Assert
            Assert.Equal(userDto, result);
        }

        [Fact]
        public async Task GetUserDetailsByEmailAsync_ValidEmail_ReturnsUserDto()
        {
            // Arrange
            var email = "test@test.com";
            var user = new User { UserId = 1, Email = email };
            var userDto = new UserDto { UserId = 1, Email = email };

            _usersRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(email)).ReturnsAsync(user);
            _mapperMock.Setup(mapper => mapper.Map<UserDto>(user)).Returns(userDto);

            var service = new UserService(_usersRepositoryMock.Object, _mapperMock.Object);

            // Act
            var result = await service.GetUserDetailsByEmailAsync(email);

            // Assert
            Assert.Equal(userDto, result);
        }
    }
}
