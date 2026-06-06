using MediatR;
using Rythm.Application.Features.Artists.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Artists.Queries
{
    public class GetAllArtistsQuery : IRequest<List<ResultArtistDto>>
    {
    }
}
