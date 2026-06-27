using MediatR;
using Microsoft.AspNetCore.Identity;
using Rythm.Application.Features.Users.Commands;
using Rythm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Users.Handlers
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand>
    {
        private readonly UserManager<AppUser> _userManager;

        public UpdateUserProfileCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.ProfileImageUrl = request.ProfileImageUrl;
            user.BirthDate = request.BirthDate;

            await _userManager.UpdateAsync(user);
        }
    }
}
