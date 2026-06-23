using AutoMapper;
using Rythm.Application.Features.Users.DTOs;
using Rythm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser, ResultUserDto>()
           .ForMember(dest => dest.Roles, opt => opt.Ignore());
        }
    }
}
