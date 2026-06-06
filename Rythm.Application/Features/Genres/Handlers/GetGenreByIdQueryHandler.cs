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
    public class GetGenreByIdQueryHandler : IRequestHandler<GetGenreByIdQuery, GetGenreByIdDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetGenreByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetGenreByIdDto?> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
        {
            var value = await _unitOfWork.Genres.GetByIdAsync(request.Id);
            return _mapper.Map<GetGenreByIdDto>(value);
        }
    }
}
