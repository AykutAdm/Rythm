using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rythm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Persistence.Configurations
{
    public class UserFollowArtistConfiguration : IEntityTypeConfiguration<UserFollowArtist>
    {
        public void Configure(EntityTypeBuilder<UserFollowArtist> builder)
        {
            builder.HasKey(x => new { x.AppUserId, x.ArtistId });
        }
    }
}
