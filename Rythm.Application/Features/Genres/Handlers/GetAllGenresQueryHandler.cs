using AutoMapper;
using MediatR;
using Rythm.Application.Features.Genres.DTOs;
using Rythm.Application.Features.Genres.Queries;
using Rythm.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Genres.Handlers
{
    public class GetAllGenresQueryHandler : IRequestHandler<GetAllGenresQuery, List<ResultGenreDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllGenresQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ResultGenreDto>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
        {
            var values = await _unitOfWork.Genres.GetAllAsync();
            return _mapper.Map<List<ResultGenreDto>>(values);
        }
    }
}
