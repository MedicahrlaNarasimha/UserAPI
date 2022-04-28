using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagementAPI.Dtos;
using UserManagementAPI.Models;

namespace UserManagementAPI.AutoMapper
{
    public class AutomapperProfiler : Profile
    {
        public AutomapperProfiler()
        {
            CreateMap<CreateUserRequestDto, User>();
        }
    }
}
