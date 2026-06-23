using AutoMapper;
using Rythm.Application.Features.Songs.Commands;
using Rythm.Application.Features.Songs.DTOs;
using Rythm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Mappings
{
    public class SongProfile : Profile
    {
        public SongProfile()
        {
            CreateMap<Song, ResultSongDto>()
                .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Artist.Name))
                .ForMember(dest => dest.AlbumTitle, opt => opt.MapFrom(src => src.Album.Title))
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name));

            CreateMap<Song, GetSongByIdDto>()
                .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Artist.Name))
                .ForMember(dest => dest.AlbumTitle, opt => opt.MapFrom(src => src.Album.Title))
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name));

            CreateMap<CreateSongCommand, Song>().ReverseMap();
            CreateMap<UpdateSongCommand, Song>().ReverseMap();
        }
    }
}
