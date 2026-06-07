using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Playlists.Commands
{
    public class CreatePlaylistCommand : IRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? CoverImageUrl { get; set; }
        public bool IsPublic { get; set; }
        public int AppUserId { get; set; }
    }
}
