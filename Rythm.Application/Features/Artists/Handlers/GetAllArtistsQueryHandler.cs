using AutoMapper;
using MediatR;
using Rythm.Application.Features.Artists.DTOs;
using Rythm.Application.Features.Artists.Queries;
using Rythm.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Artists.Handlers
{
    public class GetAllArtistsQueryHandler : IRequestHandler<GetAllArtistsQuery, List<ResultArtistDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private const string CacheKey = "artists_all";

        public GetAllArtistsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<List<ResultArtistDto>> Handle(GetAllArtistsQuery request, CancellationToken cancellationToken)
        {
            // Check Redis first
            var cachedArtists = await _cacheService.GetAsync<List<ResultArtistDto>>(CacheKey);

            // If it exists in Redis, return it directly
            if (cachedArtists != null)
            {
                return cachedArtists;
            }

            // If it's not in Redis, get it from the db
            var values = await _unitOfWork.Artists.GetAllAsync();
            var result= _mapper.Map<List<ResultArtistDto>>(values);

            // Save what we retrieved from the database to Redis
            await _cacheService.SetAsync(CacheKey, result, TimeSpan.FromMinutes(10));

            return result;
        }
    }
}
