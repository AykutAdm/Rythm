using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Rythm.Application.Features.Users.DTOs;
using Rythm.Application.Features.Users.Queries;
using Rythm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Users.Handlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<ResultUserDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<ResultUserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.ToList();
            var result = new List<ResultUserDto>();

            foreach (var user in users)
            {
                var dto = _mapper.Map<ResultUserDto>(user);
                var roles = await _userManager.GetRolesAsync(user);
                dto.Roles = roles.ToList();
                result.Add(dto);
            }

            return result;
        }
    }
}
