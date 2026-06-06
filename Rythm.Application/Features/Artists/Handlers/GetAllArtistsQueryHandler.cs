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

        public GetAllArtistsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ResultArtistDto>> Handle(GetAllArtistsQuery request, CancellationToken cancellationToken)
        {
            var values = await _unitOfWork.Artists.GetAllAsync();
            return _mapper.Map<List<ResultArtistDto>>(values);
        }
    }
}
