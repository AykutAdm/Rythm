using AutoMapper;
using MediatR;
using Rythm.Application.Features.Songs.DTOs;
using Rythm.Application.Features.Songs.Queries;
using Rythm.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Songs.Handlers
{
    public class GetAllSongsQueryHandler : IRequestHandler<GetAllSongsQuery, List<ResultSongDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private const string CacheKey = "songs_all";

        public GetAllSongsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<List<ResultSongDto>> Handle(GetAllSongsQuery request, CancellationToken cancellationToken)
        {
            // Check Redis first
            var cachedSongs = await _cacheService.GetAsync<List<ResultSongDto>>(CacheKey);

            // If it exists in Redis, return it directly
            if (cachedSongs != null)
            {
                return cachedSongs;
            }

            // If it's not in Redis, get it from the db
            var values = await _unitOfWork.Songs.GetAllAsync();
            var result = _mapper.Map<List<ResultSongDto>>(values);

            // Save what we retrieved from the database to Redis
            await _cacheService.SetAsync(CacheKey, result, TimeSpan.FromMinutes(10));

            return result;
        }
    }
}
