using MediatR;
using Rythm.Application.Features.Genres.Commands;
using Rythm.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Genres.Handlers
{
    public class RemoveGenreCommandHandler : IRequestHandler<RemoveGenreCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public RemoveGenreCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task Handle(RemoveGenreCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Genres.DeleteAsync(request.Id);
            await _unitOfWork.SaveChangesAsync();

            //clear the cache after delete
            await _cacheService.RemoveAsync("genres_all");
        }
    }
}
