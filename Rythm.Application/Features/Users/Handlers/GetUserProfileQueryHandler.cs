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
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public GetUserProfileQueryHandler(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserProfileDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            var result = _mapper.Map<UserProfileDto>(user);
            var roles = await _userManager.GetRolesAsync(user);
            result.Roles = roles.ToList();
            return result;
        }
    }
}
