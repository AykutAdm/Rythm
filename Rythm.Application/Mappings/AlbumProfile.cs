using AutoMapper;
using Rythm.Application.Features.Albums.Commands;
using Rythm.Application.Features.Albums.DTOs;
using Rythm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Mappings
{
    public class AlbumProfile : Profile
    {
        public AlbumProfile()
        {
            CreateMap<Album, ResultAlbumDto>()
           .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Artist.Name));

            CreateMap<Album, GetAlbumByIdDto>()
                .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Artist.Name))
                .ForMember(dest => dest.Songs, opt => opt.MapFrom(src => src.Songs));

            CreateMap<CreateAlbumCommand, Album>();
            CreateMap<UpdateAlbumCommand, Album>();
        }
    }
}
