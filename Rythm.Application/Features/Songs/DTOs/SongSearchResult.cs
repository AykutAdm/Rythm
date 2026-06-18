using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Songs.DTOs
{
    public class SongSearchResult
    {
        public int SongId { get; set; }
        public string Title { get; set; }
        public string ArtistName { get; set; }
        public string? AlbumTitle { get; set; }
        public string? GenreName { get; set; }
    }
}
