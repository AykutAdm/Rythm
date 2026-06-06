using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Albums.Commands
{
    public class UpdateAlbumCommand : IRequest
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public string? CoverImageUrl { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
