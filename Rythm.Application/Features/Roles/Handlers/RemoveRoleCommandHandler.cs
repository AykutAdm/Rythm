using MediatR;
using Microsoft.AspNetCore.Identity;
using Rythm.Application.Features.Roles.Commands;
using Rythm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Roles.Handlers
{
    public class RemoveRoleCommandHandler : IRequestHandler<RemoveRoleCommand>
    {
        private readonly UserManager<AppUser> _userManager;

        public RemoveRoleCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            await _userManager.RemoveFromRoleAsync(user, request.Role);
        }
    }
}
