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

        public GetAllAlbumsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ResultAlbumDto>> Handle(GetAllAlbumsQuery request, CancellationToken cancellationToken)
        {
            var values = await _unitOfWork.Albums.GetAllAsync();
            return _mapper.Map<List<ResultAlbumDto>>(values);
        }
    }
}
