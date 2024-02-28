using AutoMapper;
using DoorManagementSystem.Application.DTOs;
using DoorManagementSystem.Domain.Entities;

namespace DoorManagementSystem.Application.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDto, Users>().ReverseMap();
            CreateMap<AccessRequestDto, RoleDoorAccess>().ReverseMap();

        }
    }
}
