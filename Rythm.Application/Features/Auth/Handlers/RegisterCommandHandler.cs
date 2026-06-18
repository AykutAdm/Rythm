using MediatR;
using Microsoft.AspNetCore.Identity;
using Rythm.Application.Features.Auth.Commands;
using Rythm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Auth.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public RegisterCommandHandler(UserManager<AppUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new AppUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Email = request.Email,
                BirthDate = request.BirthDate,
                CreatedAt = DateTime.Now
            };

            await _userManager.CreateAsync(user, request.Password);

            // Rol yoksa oluştur, varsa geç
            if (!await _roleManager.RoleExistsAsync("User"))
                await _roleManager.CreateAsync(new IdentityRole<int>("User"));

            // Kullanıcıya User rolünü ver
            await _userManager.AddToRoleAsync(user, "User");
        }
    }
}
