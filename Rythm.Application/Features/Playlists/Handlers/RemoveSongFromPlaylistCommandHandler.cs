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
    public class RemoveSongFromPlaylistCommandHandler : IRequestHandler<RemoveSongFromPlaylistCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveSongFromPlaylistCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RemoveSongFromPlaylistCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Playlists.RemoveSongAsync(request.PlaylistId, request.SongId);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
