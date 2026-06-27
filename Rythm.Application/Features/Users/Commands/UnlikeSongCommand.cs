using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Users.Commands
{
    public class UnlikeSongCommand : IRequest
    {
        public int AppUserId { get; set; }
        public int SongId { get; set; }
    }
}
