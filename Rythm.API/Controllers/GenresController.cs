using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rythm.Application.Features.Genres.Commands;
using Rythm.Application.Features.Genres.Queries;

namespace Rythm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GenresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var result = await _mediator.Send(new GetAllGenresQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            var result = await _mediator.Send(new GetGenreByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenre(CreateGenreCommand command)
        {
            await _mediator.Send(command);
            return Ok(new { message = "Şarkı türü oluşturuldu." });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGenre(UpdateGenreCommand command)
        {
            await _mediator.Send(command);
            return Ok(new { message = "Şarkı türü güncellendi." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveGenre(int id)
        {
            await _mediator.Send(new RemoveGenreCommand(id));
            return Ok(new { message = "Şarkı Türü silindi." });
        }
    }
}
