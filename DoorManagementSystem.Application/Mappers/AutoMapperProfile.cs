using AutoMapper;
using DoorManagementSystem.Application.DTOs;
using DoorManagementSystem.Domain.Entities;

namespace DoorManagementSystem.Application.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<AccessRequestDto, RoleDoor>().ReverseMap();
            CreateMap<UserDto,User>().ReverseMap();
            CreateMap<AccessLogDto, DoorLog>().ReverseMap();


        }
    }
}
