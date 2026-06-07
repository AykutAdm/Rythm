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
    public class GetAllPlaylistsQueryHandler : IRequestHandler<GetAllPlaylistsQuery, List<ResultPlaylistDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllPlaylistsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ResultPlaylistDto>> Handle(GetAllPlaylistsQuery request, CancellationToken cancellationToken)
        {
            var values = await _unitOfWork.Playlists.GetAllAsync();
            return _mapper.Map<List<ResultPlaylistDto>>(values);
        }
    }
}
