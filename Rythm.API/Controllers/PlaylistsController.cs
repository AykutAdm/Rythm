using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rythm.Application.Features.Playlists.Commands;
using Rythm.Application.Features.Playlists.Queries;

namespace Rythm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlaylistsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlaylists()
        {
            var result = await _mediator.Send(new GetAllPlaylistsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlaylistById(int id)
        {
            var result = await _mediator.Send(new GetPlaylistByIdQuery(id));
            if (result == null) return NotFound("Playlist bulunamadı.");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlaylist(CreatePlaylistCommand command)
        {
            await _mediator.Send(command);
            return Ok("Playlist oluşturuldu.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePlaylist(UpdatePlaylistCommand command)
        {
            await _mediator.Send(command);
            return Ok("Playlist güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemovePlaylist(int id)
        {
            await _mediator.Send(new RemovePlaylistCommand(id));
            return Ok("Playlist silindi.");
        }
    }
}
