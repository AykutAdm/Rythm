using Rythm.Application.Features.Artists.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Playlists.DTOs
{
    public class GetPlaylistByIdDto
    {
        public int PlaylistId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? CoverImageUrl { get; set; }
        public bool IsPublic { get; set; }
        public List<SongSummaryDto> Songs { get; set; }
    }
}
