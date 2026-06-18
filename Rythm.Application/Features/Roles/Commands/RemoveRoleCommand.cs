using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Roles.Commands
{
    public class RemoveRoleCommand : IRequest
    {
        public int UserId { get; set; }
        public string Role { get; set; }

        public RemoveRoleCommand(int userId, string role)
        {
            UserId = userId;
            Role = role;
        }

       
    }
}
