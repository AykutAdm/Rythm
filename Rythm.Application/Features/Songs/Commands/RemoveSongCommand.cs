using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Songs.Commands
{
    public class RemoveSongCommand : IRequest
    {
        public int Id { get; set; }

        public RemoveSongCommand(int id)
        {
            Id = id;
        }
    }
}
