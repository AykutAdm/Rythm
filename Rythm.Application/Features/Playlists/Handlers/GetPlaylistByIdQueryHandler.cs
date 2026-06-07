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
    public class GetPlaylistByIdQueryHandler : IRequestHandler<GetPlaylistByIdQuery, GetPlaylistByIdDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPlaylistByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetPlaylistByIdDto> Handle(GetPlaylistByIdQuery request, CancellationToken cancellationToken)
        {
            var value = await _unitOfWork.Playlists.GetByIdAsync(request.Id);
            return _mapper.Map<GetPlaylistByIdDto>(value);
        }
    }
}
