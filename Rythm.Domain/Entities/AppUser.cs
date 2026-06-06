using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Domain.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public Artist? Artist { get; set; }
        public List<Playlist> Playlists { get; set; }
        public List<UserLikedSong> LikedSongs { get; set; }
        public List<UserFollowArtist> FollowedArtists { get; set; }
    }
}
