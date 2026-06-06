using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Domain.Entities
{
    public class UserFollowArtist
    {
        public int AppUserId { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

        public DateTime FollowedAt { get; set; } = DateTime.UtcNow;
    }
}
