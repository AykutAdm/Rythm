using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Domain.Entities
{
    public class UserLikedSong
    {
        public int AppUserId { get; set; }
        public int SongId { get; set; }
        public Song Song { get; set; }

        public DateTime LikedAt { get; set; } = DateTime.UtcNow;
    }
}
