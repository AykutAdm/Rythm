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
        private readonly ISearchService _searchService;

        public CreateSongCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService, ISearchService searchService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
            _searchService = searchService;
        }

        public async Task Handle(CreateSongCommand request, CancellationToken cancellationToken)
        {
            var value = _mapper.Map<Song>(request);
            value.CreatedAt = DateTime.UtcNow;
            await _unitOfWork.Songs.AddAsync(value);
            await _unitOfWork.SaveChangesAsync();

            //add to elasticsearch
            var songWithDetails = await _unitOfWork.Songs.GetByIdAsync(value.SongId);
            await _searchService.IndexSongAsync(songWithDetails!.SongId,songWithDetails.Title,songWithDetails.Artist?.Name ?? string.Empty,songWithDetails.Album?.Title,songWithDetails.Genre?.Name);

            // New song added, clear cache
            await _cacheService.RemoveAsync("songs_all");
        }
    }
}
