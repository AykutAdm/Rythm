using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Albums.Commands
{
    public class RemoveAlbumCommand : IRequest
    {
        public int Id { get; set; }
        public RemoveAlbumCommand(int id)
        {
            Id = id;
        }
    }
}
