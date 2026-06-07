using MediatR;
using Rythm.Application.Features.Artists.Commands;
using Rythm.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Artists.Handlers
{
    public class RemoveArtistCommandHandler : IRequestHandler<RemoveArtistCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public RemoveArtistCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task Handle(RemoveArtistCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Artists.DeleteAsync(request.Id);
            await _unitOfWork.SaveChangesAsync();

            // The artist has been deleted, clear the cache
            await _cacheService.RemoveAsync("artists_all");
        }
    }
}
