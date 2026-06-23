using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rythm.Application.Features.Artists.Commands;
using Rythm.Application.Features.Artists.Queries;

namespace Rythm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ArtistsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArtists()
        {
            var result = await _mediator.Send(new GetAllArtistsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtistById(int id)
        {
            var result = await _mediator.Send(new GetArtistByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateArtist(CreateArtistCommand command)
        {
            await _mediator.Send(command);
            return Ok(new { message = "Sanatçı oluşturuldu." });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateArtist(UpdateArtistCommand command)
        {
            await _mediator.Send(command);
            return Ok(new { message = "Sanatçı güncellendi." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveArtist(int id)
        {
            await _mediator.Send(new RemoveArtistCommand(id));
            return Ok(new { message = "Sanatçı silindi." });
        }
    }
}
