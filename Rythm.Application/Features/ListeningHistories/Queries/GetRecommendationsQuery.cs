using MediatR;
using Rythm.Application.Features.Songs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.ListeningHistories.Queries
{
    public class GetRecommendationsQuery : IRequest<List<ResultSongDto>>
    {
        public int AppUserId { get; set; }
        public GetRecommendationsQuery(int appUserId)
        {
            AppUserId = appUserId;
        }
    }
}
