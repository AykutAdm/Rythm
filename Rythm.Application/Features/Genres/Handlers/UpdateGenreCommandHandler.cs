using AutoMapper;
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
    public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public UpdateGenreCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
            var value = await _unitOfWork.Genres.GetByIdAsync(request.GenreId);
            _mapper.Map(request, value);
            await _unitOfWork.Genres.UpdateAsync(value);
            await _unitOfWork.SaveChangesAsync();

            //clear the cache after update
            await _cacheService.RemoveAsync("genres_all");

        }
    }
}
