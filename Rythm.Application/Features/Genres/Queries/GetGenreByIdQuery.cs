using MediatR;
using Rythm.Application.Features.Genres.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Genres.Queries
{
    public class GetGenreByIdQuery : IRequest<GetGenreByIdDto>
    {
        public int Id { get; set; }
        public GetGenreByIdQuery(int id)
        {
            Id = id;
        }
    }
}
