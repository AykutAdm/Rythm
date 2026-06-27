using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Dashboard.DTOs
{
    public class DashboardStatsDto
    {
        public int TotalSongs { get; set; }
        public int TotalArtists { get; set; }
        public int TotalAlbums { get; set; }
        public int TotalGenres { get; set; }
        public int TotalPlaylists { get; set; }
        public int TotalUsers { get; set; }
    }
}
