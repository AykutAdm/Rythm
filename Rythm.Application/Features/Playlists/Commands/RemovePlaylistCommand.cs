using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Playlists.Commands
{
    public class RemovePlaylistCommand : IRequest
    {
        public int Id { get; set; }
        public RemovePlaylistCommand(int id)
        {
            Id = id;
        }
    }
}
