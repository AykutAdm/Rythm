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
    public class GetArtistByIdQueryHandler : IRequestHandler<GetArtistByIdQuery, GetArtistByIdDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetArtistByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetArtistByIdDto> Handle(GetArtistByIdQuery request, CancellationToken cancellationToken)
        {
            var value = await _unitOfWork.Artists.GetByIdAsync(request.Id);
            return _mapper.Map<GetArtistByIdDto>(value);
        }
    }
}
