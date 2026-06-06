using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Domain.Entities
{
    public class Song
    {
        public int SongId { get; set; }
        public string Title { get; set; }
        public string? CoverImageUrl { get; set; }
        public string AudioUrl { get; set; }
        public int DurationInSeconds { get; set; }
        public int PlayCount { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

        public int? AlbumId { get; set; }
        public Album? Album { get; set; }

        public int? GenreId { get; set; }
        public Genre? Genre { get; set; }

        public List<PlaylistSong> PlaylistSongs { get; set; }
        public List<UserLikedSong> LikedByUsers { get; set; }
    }
}
