using AutoMapper;
using DoorManagementSystem.Application.DTOs;
using DoorManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorManagementSystem.Application.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDto,Users>().ReverseMap();
            CreateMap<AccessRequestDto, RoleDoorAccess>().ReverseMap();

        }
    }
}
