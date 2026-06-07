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
    public class RemovePlaylistCommandHandler : IRequestHandler<RemovePlaylistCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemovePlaylistCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RemovePlaylistCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Playlists.DeleteAsync(request.Id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
