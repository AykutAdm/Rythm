using MediatR;
using Rythm.Application.Features.Playlists.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Playlists.Queries
{
    public class GetPlaylistsByUserIdQuery : IRequest<List<ResultPlaylistDto>>
    {
        public int AppUserId { get; set; }
        public GetPlaylistsByUserIdQuery(int appUserId)
        {
            AppUserId = appUserId;
        }
    }
}
