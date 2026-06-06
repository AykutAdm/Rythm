using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Artists.DTOs
{
    public class GetArtistByIdDto
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public string? Bio { get; set; }
        public string? ProfileImageUrl { get; set; }
        public List<AlbumSummaryDto> Albums { get; set; }
        public List<SongSummaryDto> Songs { get; set; }
    }

    public class AlbumSummaryDto
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
    }

    public class SongSummaryDto
    {
        public int SongId { get; set; }
        public string Title { get; set; }
    }
}
