using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Domain.Entities
{
    public class Artist
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public string? Bio { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? AppUserId { get; set; }

        public List<Album> Albums { get; set; }
        public List<Song> Songs { get; set; }
        public List<UserFollowArtist> Followers { get; set; }
    }
}
