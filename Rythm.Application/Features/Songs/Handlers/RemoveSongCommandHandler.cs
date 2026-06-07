using MediatR;
using Rythm.Application.Features.Songs.Commands;
using Rythm.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Songs.Handlers
{
    public class RemoveSongCommandHandler : IRequestHandler<RemoveSongCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public RemoveSongCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task Handle(RemoveSongCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Songs.DeleteAsync(request.Id);
            await _unitOfWork.SaveChangesAsync();

            // The song has been deleted, clear the cache
            await _cacheService.RemoveAsync("songs_all");
        }
    }
}
