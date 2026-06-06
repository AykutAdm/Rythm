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
    public class GetAlbumByIdQueryHandler : IRequestHandler<GetAlbumByIdQuery, GetAlbumByIdDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAlbumByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetAlbumByIdDto> Handle(GetAlbumByIdQuery request, CancellationToken cancellationToken)
        {
            var value = await _unitOfWork.Albums.GetByIdAsync(request.Id);
            return _mapper.Map<GetAlbumByIdDto>(value);
        }
    }
}
