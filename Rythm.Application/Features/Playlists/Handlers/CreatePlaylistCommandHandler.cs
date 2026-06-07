using AutoMapper;
using MediatR;
using Rythm.Application.Features.Playlists.Commands;
using Rythm.Application.Interfaces;
using Rythm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Playlists.Handlers
{
    public class CreatePlaylistCommandHandler : IRequestHandler<CreatePlaylistCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePlaylistCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(CreatePlaylistCommand request, CancellationToken cancellationToken)
        {
            var value = _mapper.Map<Playlist>(request);
            value.CreatedAt = DateTime.Now;
            await _unitOfWork.Playlists.AddAsync(value);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
