using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Albums.DTOs
{
    public class ResultAlbumDto
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public string? CoverImageUrl { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ArtistName { get; set; }
    }
}
