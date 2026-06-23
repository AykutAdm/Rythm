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
    public class AddSongToPlaylistCommandHandler : IRequestHandler<AddSongToPlaylistCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddSongToPlaylistCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(AddSongToPlaylistCommand request, CancellationToken cancellationToken)
        {
            var playlistSong = new PlaylistSong
            {
                PlaylistId = request.PlaylistId,
                SongId = request.SongId,
                Order = request.Order,
                AddedAt = DateTime.Now
            };
            await _unitOfWork.Playlists.AddSongAsync(playlistSong);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
