using AutoMapper;
using MediatR;
using Rythm.Application.Features.Playlists.Commands;
using Rythm.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Playlists.Handlers
{
    public class UpdatePlaylistCommandHandler : IRequestHandler<UpdatePlaylistCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePlaylistCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdatePlaylistCommand request, CancellationToken cancellationToken)
        {
            var value = await _unitOfWork.Playlists.GetByIdAsync(request.PlaylistId);
            _mapper.Map(request, value);
            await _unitOfWork.Playlists.UpdateAsync(value);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
