using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rythm.Application.Features.ListeningHistories.Commands;
using Rythm.Application.Features.ListeningHistories.Queries;
using Rythm.Application.Interfaces;

namespace Rythm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HistoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateListeningHistoryCommand command)
        {
            await _mediator.Send(command);
            return Ok(new { message = "Dinleme geçmişi kaydedildi." });
        }

        [HttpGet("recommendations/{userId}")]
        public async Task<IActionResult> GetRecommendations(int userId)
        {
            var result = await _mediator.Send(new GetRecommendationsQuery(userId));
            return Ok(result);
        }


        [HttpPost("retrain")]
        public async Task<IActionResult> Retrain(IRecommendationService service)
        {
            await service.TrainModelAsync();
            return Ok(new { message = "Model yeniden eğitildi." });
        }
    }
}
