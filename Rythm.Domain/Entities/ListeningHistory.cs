using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Domain.Entities
{
    public class ListeningHistory
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public int SongId { get; set; }
        public Song Song { get; set; }
        public DateTime ListenedAt { get; set; }
    }
}
