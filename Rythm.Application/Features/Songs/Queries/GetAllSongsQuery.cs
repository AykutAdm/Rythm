using MediatR;
using Rythm.Application.Features.Songs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Songs.Queries
{
    public class GetAllSongsQuery : IRequest<List<ResultSongDto>>
    {
    }
}
