using AutoMapper;
using MediatR;
using Rythm.Application.Features.Albums.DTOs;
using Rythm.Application.Features.Albums.Queries;
using Rythm.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Albums.Handlers
{
    public class GetAllAlbumsQueryHandler : IRequestHandler<GetAllAlbumsQuery, List<ResultAlbumDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private const string CacheKey = "albums_all";

        public GetAllAlbumsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<List<ResultAlbumDto>> Handle(GetAllAlbumsQuery request, CancellationToken cancellationToken)
        {
            // Check Redis first
            var cachedAlbums = await _cacheService.GetAsync<List<ResultAlbumDto>>(CacheKey);

            // If it exists in Redis, return it directly
            if (cachedAlbums != null)
            {
                return cachedAlbums;
            }

            // If it's not in Redis, get it from the db
            var values = await _unitOfWork.Albums.GetAllAsync();
            var result = _mapper.Map<List<ResultAlbumDto>>(values);

            // Save what we retrieved from the database to Redis
            await _cacheService.SetAsync(CacheKey, result, TimeSpan.FromMinutes(10));

            return result;
        }
    }
}
