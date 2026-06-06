using AutoMapper;
using MediatR;
using Rythm.Application.Features.Albums.Commands;
using Rythm.Application.Interfaces;
using Rythm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Albums.Handlers
{
    public class CreateAlbumCommandHandler : IRequestHandler<CreateAlbumCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAlbumCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(CreateAlbumCommand request, CancellationToken cancellationToken)
        {
            var value = _mapper.Map<Album>(request);
            value.CreatedAt = DateTime.Now;
            await _unitOfWork.Albums.AddAsync(value);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
