using MediatR;
using Rythm.Application.Features.Playlists.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Playlists.Queries
{
    public class GetPlaylistByIdQuery : IRequest<GetPlaylistByIdDto>
    {
        public int Id { get; set; }
        public GetPlaylistByIdQuery(int id)
        {
            Id = id;
        }
    }
}
