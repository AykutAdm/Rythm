using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Artists.Commands
{
    public class UpdateArtistCommand : IRequest
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public string? Bio { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
