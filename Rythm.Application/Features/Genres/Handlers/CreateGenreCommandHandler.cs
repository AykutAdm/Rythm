using AutoMapper;
using MediatR;
using Rythm.Application.Features.Genres.Commands;
using Rythm.Application.Interfaces;
using Rythm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Genres.Handlers
{
    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateGenreCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var value = _mapper.Map<Genre>(request);
            await _unitOfWork.Genres.AddAsync(value);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
