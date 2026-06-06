using MediatR;
using Rythm.Application.Features.Albums.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Albums.Queries
{
    public class GetAllAlbumsQuery : IRequest<List<ResultAlbumDto>>
    {
    }
}
