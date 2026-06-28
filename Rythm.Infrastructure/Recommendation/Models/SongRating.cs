using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Infrastructure.Recommendation.Models
{
    public class SongRating
    {
        [KeyType(count: 100_000)]
        public uint UserId;
        [KeyType(count: 100_000)]
        public uint SongId;
        public float Label;
    }
}
