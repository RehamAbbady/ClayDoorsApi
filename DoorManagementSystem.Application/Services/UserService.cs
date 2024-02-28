using AutoMapper;
using DoorManagementSystem.Application.DTOs;
using DoorManagementSystem.Application.Interfaces.IRepositories;
using DoorManagementSystem.Application.Interfaces.IServices;
using DoorManagementSystem.Domain.Entities;


namespace DoorManagementSystem.Application.Services
{
    public class UserService : IUsersService
    {
        private readonly IUsersRepository _userRepository;
        private readonly IMapper _mapper;


        public UserService(IUsersRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);

        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDto>(user);

        }
        public async Task<UserDto?> GetUserDetailsByEmailAsync(string email)
        {
            var user= await _userRepository.GetUserByEmailAsync(email);
            return _mapper.Map<UserDto?>(user);

        }
        public async Task<bool> RemoveRoleFromUserAsync(int userId, int roleId)
        {
            return await _userRepository.RemoveRoleFromUserAsync(userId, roleId);
        }
        public async Task<bool> AddRoleToUserAsync(int userId, int roleId)
        {
            return await _userRepository.AddRoleToUserAsync(userId, roleId);
        }
        public async Task<bool> IsUserAdminForDoorAsync(int userId, int doorId)
        {
            return await _userRepository.IsUserAdminForDoorAsync(userId, doorId);

        }


    }

}
