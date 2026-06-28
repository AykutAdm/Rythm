using AutoMapper;
using MediatR;
using Rythm.Application.Features.ListeningHistories.Queries;
using Rythm.Application.Features.Songs.DTOs;
using Rythm.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.ListeningHistories.Handlers
{
    public class GetRecommendationsQueryHandler : IRequestHandler<GetRecommendationsQuery, List<ResultSongDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRecommendationService _recommendationService;

        public GetRecommendationsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IRecommendationService recommendationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _recommendationService = recommendationService;
        }

        public async Task<List<ResultSongDto>> Handle(GetRecommendationsQuery request, CancellationToken cancellationToken)
        {

            var songIds = await _recommendationService.GetRecommendedSongIdsAsync(request.AppUserId, 10);

            if (songIds.Count == 0)
            {
                var history = await _unitOfWork.ListeningHistories.GetByUserIdAsync(request.AppUserId);

                var topArtistIds = history.GroupBy(x => x.Song.ArtistId).OrderByDescending(x => x.Count()).Take(3).Select(x => x.Key).ToList();

                var listenedSongIds = history.Select(x => x.SongId).Distinct().ToList();

                var values = await _unitOfWork.Songs.GetAllAsync();
                var sqlrecommendations = values.Where(x => topArtistIds.Contains(x.ArtistId) && !listenedSongIds.Contains(x.SongId)).Take(10).ToList();

                return _mapper.Map<List<ResultSongDto>>(sqlrecommendations);
            }

            var allSongs = await _unitOfWork.Songs.GetAllAsync();
            var recommendations = allSongs.Where(s => songIds.Contains(s.SongId)).ToList();
            return _mapper.Map<List<ResultSongDto>>(recommendations);
        }
    }
}
