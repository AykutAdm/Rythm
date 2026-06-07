using AutoMapper;
using MediatR;
using Rythm.Application.Features.Songs.Commands;
using Rythm.Application.Interfaces;
using Rythm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Songs.Handlers
{
    public class CreateSongCommandHandler : IRequestHandler<CreateSongCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public CreateSongCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task Handle(CreateSongCommand request, CancellationToken cancellationToken)
        {
            var value = _mapper.Map<Song>(request);
            value.CreatedAt = DateTime.UtcNow;
            await _unitOfWork.Songs.AddAsync(value);
            await _unitOfWork.SaveChangesAsync();

            // New song added, clear cache
            await _cacheService.RemoveAsync("songs_all");
        }
    }
}
