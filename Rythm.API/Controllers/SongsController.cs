using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rythm.Application.Features.Songs.Commands;
using Rythm.Application.Features.Songs.Queries;

namespace Rythm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SongsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSongs()
        {
            var result = await _mediator.Send(new GetAllSongsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSongById(int id)
        {
            var result = await _mediator.Send(new GetSongByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSong(CreateSongCommand command)
        {
            await _mediator.Send(command);
            return Ok(new { message = "Şarkı oluşturuldu." });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSong(UpdateSongCommand command)
        {
            await _mediator.Send(command);
            return Ok(new { message = "Şarkı güncellendi." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveSong(int id)
        {
            await _mediator.Send(new RemoveSongCommand(id));
            return Ok(new { message = "Şarkı silindi." });
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string query)
        {
            var result = await _mediator.Send(new SearchSongsQuery(query));
            return Ok(result);
        }
    }
}
