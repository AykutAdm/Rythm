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
    public class GetAllSongsQueryHandler : IRequestHandler<GetAllSongsQuery, List<ResultSongDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllSongsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ResultSongDto>> Handle(GetAllSongsQuery request, CancellationToken cancellationToken)
        {
            var values = await _unitOfWork.Songs.GetAllAsync();
            return _mapper.Map<List<ResultSongDto>>(values);
        }
    }
}
