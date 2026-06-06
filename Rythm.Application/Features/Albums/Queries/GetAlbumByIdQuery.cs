using MediatR;
using Rythm.Application.Features.Albums.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Albums.Queries
{
    public class GetAlbumByIdQuery : IRequest<GetAlbumByIdDto>
    {
        public int Id { get; set; }
        public GetAlbumByIdQuery(int id)
        {
            Id = id;
        }
    }
}
