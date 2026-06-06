using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Artists.Commands
{
    public class RemoveArtistCommand : IRequest
    {
        public int Id { get; set; }
        public RemoveArtistCommand(int id)
        {
            Id = id;
        }
    }
}
