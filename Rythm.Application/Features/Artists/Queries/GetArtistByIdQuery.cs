using MediatR;
using Rythm.Application.Features.Artists.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Artists.Queries
{
    public class GetArtistByIdQuery : IRequest<GetArtistByIdDto>
    {
        public int Id { get; set; }
        public GetArtistByIdQuery(int id)
        {
            Id = id;
        }
    }
}
