using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Songs.Commands
{
    public class UpdateSongCommand : IRequest
    {
        public int SongId { get; set; }
        public string Title { get; set; }
        public string? CoverImageUrl { get; set; }
        public string AudioUrl { get; set; }
        public int DurationInSeconds { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int? AlbumId { get; set; }
        public int? GenreId { get; set; }
    }
}
