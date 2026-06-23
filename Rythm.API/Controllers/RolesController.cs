using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rythm.Application.Features.Roles.Commands;

namespace Rythm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole(AssignRoleCommand command)
        {
            await _mediator.Send(command);
            return Ok(new { message = "Rol verildi." });
        }

        [HttpPost("remove-role")]
        public async Task<IActionResult> RemoveRole(RemoveRoleCommand command)
        {
            await _mediator.Send(command);
            return Ok(new { message = "Rol kaldırıldı." });
        }
    }
}
