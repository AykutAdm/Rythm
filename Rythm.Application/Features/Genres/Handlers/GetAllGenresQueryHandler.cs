using AutoMapper;
using MediatR;
using Rythm.Application.Features.Genres.DTOs;
using Rythm.Application.Features.Genres.Queries;
using Rythm.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Genres.Handlers
{
    public class GetAllGenresQueryHandler : IRequestHandler<GetAllGenresQuery, List<ResultGenreDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private const string CacheKey = "genres_all";

        public GetAllGenresQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<List<ResultGenreDto>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
        {
            // Check Redis first
            var cachedGenres = await _cacheService.GetAsync<List<ResultGenreDto>>(CacheKey);

            // If it exists in Redis, return it directly
            if (cachedGenres != null)
            {
                return cachedGenres;
            }

            // If it's not in Redis, get it from the db
            var values = await _unitOfWork.Genres.GetAllAsync();
            var result = _mapper.Map<List<ResultGenreDto>>(values);

            // Save what we retrieved from the database to Redis
            await _cacheService.SetAsync(CacheKey, result, TimeSpan.FromMinutes(10));

            return result;
        }
    }
}
