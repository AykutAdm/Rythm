using MediatR;
using Rythm.Application.Features.Songs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Users.Queries
{
    public class GetLikedSongsQuery : IRequest<List<ResultSongDto>>
    {
        public int AppUserId { get; set; }
        public GetLikedSongsQuery(int appUserId)
        {
            AppUserId = appUserId;
        }
    }
}
