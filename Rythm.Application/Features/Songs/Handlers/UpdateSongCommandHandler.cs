using AutoMapper;
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
    public class UpdateSongCommandHandler : IRequestHandler<UpdateSongCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public UpdateSongCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task Handle(UpdateSongCommand request, CancellationToken cancellationToken)
        {
            var value = await _unitOfWork.Songs.GetByIdAsync(request.SongId);
            _mapper.Map(request, value);
            await _unitOfWork.Songs.UpdateAsync(value);
            await _unitOfWork.SaveChangesAsync();

            // The song has been updated, clear the cache
            await _cacheService.RemoveAsync("songs_all");
        }
    }
}
