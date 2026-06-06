using AutoMapper;
using Rythm.Application.Features.Artists.DTOs;
using Rythm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Mappings
{
    public class ArtistProfile : Profile
    {
        public ArtistProfile()
        {
            CreateMap<Artist, ResultArtistDto>();

            CreateMap<Artist, GetArtistByIdDto>()
                .ForMember(dest => dest.Albums, opt => opt.MapFrom(src => src.Albums))
                .ForMember(dest => dest.Songs, opt => opt.MapFrom(src => src.Songs));

            CreateMap<Album, AlbumSummaryDto>();
            CreateMap<Song, SongSummaryDto>();

            CreateMap<CreateArtistCommand, Artist>();
            CreateMap<UpdateArtistCommand, Artist>();
        }
    }
}
