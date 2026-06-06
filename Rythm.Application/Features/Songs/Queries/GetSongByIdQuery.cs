using MediatR;
using Rythm.Application.Features.Songs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Songs.Queries
{
    public class GetSongByIdQuery : IRequest<GetSongByIdDto>
    {
        public int Id { get; set; }

        public GetSongByIdQuery(int id)
        {
            Id = id;
        }
    }
}
