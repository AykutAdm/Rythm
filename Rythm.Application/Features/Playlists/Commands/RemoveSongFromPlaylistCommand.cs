using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Playlists.Commands
{
    public class RemoveSongFromPlaylistCommand : IRequest
    {
        public int PlaylistId { get; set; }
        public int SongId { get; set; }
    }
}
