using AutoMapper;
using MediatR;
using Rythm.Application.Features.Songs.DTOs;
using Rythm.Application.Features.Users.Queries;
using Rythm.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Users.Handlers
{
    public class GetLikedSongsQueryHandler : IRequestHandler<GetLikedSongsQuery, List<ResultSongDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLikedSongsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ResultSongDto>> Handle(GetLikedSongsQuery request, CancellationToken cancellationToken)
        {
            var likedSongs = await _unitOfWork.LikedSongs.GetByUserIdAsync(request.AppUserId);
            var result = likedSongs.Select(x => x.Song).ToList();
            return _mapper.Map<List<ResultSongDto>>(result);
        }
    }
}
