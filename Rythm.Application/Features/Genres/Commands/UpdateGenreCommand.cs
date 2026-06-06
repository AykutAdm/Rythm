using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Genres.Commands
{
    public class UpdateGenreCommand : IRequest
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
    }
}
