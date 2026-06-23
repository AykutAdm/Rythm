using AutoMapper;
using MediatR;
using Rythm.Application.Features.Playlists.DTOs;
using Rythm.Application.Features.Playlists.Queries;
using Rythm.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Playlists.Handlers
{
    public class GetPlaylistsByUserIdQueryHandler : IRequestHandler<GetPlaylistsByUserIdQuery, List<ResultPlaylistDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPlaylistsByUserIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ResultPlaylistDto>> Handle(GetPlaylistsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var playlists = await _unitOfWork.Playlists.GetByUserIdAsync(request.AppUserId);
            return _mapper.Map<List<ResultPlaylistDto>>(playlists);
        }
    }
}
