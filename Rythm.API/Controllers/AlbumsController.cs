using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rythm.Application.Features.Albums.Commands;
using Rythm.Application.Features.Albums.Queries;

namespace Rythm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AlbumsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAlbums()
        {
            var result = await _mediator.Send(new GetAllAlbumsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbumById(int id)
        {
            var result = await _mediator.Send(new GetAlbumByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlbum(CreateAlbumCommand command)
        {
            await _mediator.Send(command);
            return Ok(new { message = "Albüm oluşturuldu." });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAlbum(UpdateAlbumCommand command)
        {
            await _mediator.Send(command);
            return Ok(new { message = "Albüm güncellendi." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAlbum(int id)
        {
            await _mediator.Send(new RemoveAlbumCommand(id));
            return Ok(new { message = "Albüm silindi." });
        }
    }
}
