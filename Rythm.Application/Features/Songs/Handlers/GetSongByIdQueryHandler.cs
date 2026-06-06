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
    public class GetSongByIdQueryHandler : IRequestHandler<GetSongByIdQuery, GetSongByIdDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSongByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetSongByIdDto> Handle(GetSongByIdQuery request, CancellationToken cancellationToken)
        {
            var value = await _unitOfWork.Songs.GetByIdAsync(request.Id);
            return _mapper.Map<GetSongByIdDto>(value);
        }
    }
}
