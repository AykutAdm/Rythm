using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rythm.Application.Features.Users.Commands;
using Rythm.Application.Features.Users.Queries;

namespace Rythm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            var result = await _mediator.Send(new GetUserProfileQuery(id));
            return Ok(result);
        }

        [Authorize(Roles = "Admin,User,Premium,Artist")]
        [HttpPut]
        public async Task<IActionResult> UpdateProfile(UpdateUserProfileCommand command)
        {
            await _mediator.Send(command);
            return Ok(new { message = "Profil güncellendi." });
        }

        [HttpPost("like-song")]
        public async Task<IActionResult> LikeSong(LikeSongCommand command)
        {
            await _mediator.Send(command);
            return Ok(new { message = "Şarkı beğenildi." });
        }

        [HttpPost("unlike-song")]
        public async Task<IActionResult> UnlikeSong(UnlikeSongCommand command)
        {
            await _mediator.Send(command);
            return Ok(new { message = "Beğeni kaldırıldı." });
        }

        [HttpGet("liked-songs/{userId}")]
        public async Task<IActionResult> GetLikedSongs(int userId)
        {
            var result = await _mediator.Send(new GetLikedSongsQuery(userId));
            return Ok(result);
        }
    }
}
