using Rythm.Application.Features.Artists.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Genres.DTOs
{
    public class GetGenreByIdDto
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public List<SongSummaryDto> Songs { get; set; }
    }
}
