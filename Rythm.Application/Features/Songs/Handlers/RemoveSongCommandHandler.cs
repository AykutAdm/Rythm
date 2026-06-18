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
        private readonly ISearchService _searchService;

        public RemoveSongCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService, ISearchService searchService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
            _searchService = searchService;
        }

        public async Task Handle(RemoveSongCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Songs.DeleteAsync(request.Id);
            await _unitOfWork.SaveChangesAsync();

            //remove from the elasticsearch
            await _searchService.DeleteSongAsync(request.Id);

            // The song has been deleted, clear the cache
            await _cacheService.RemoveAsync("songs_all");
        }
    }
}
