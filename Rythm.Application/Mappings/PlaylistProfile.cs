using AutoMapper;
using Rythm.Application.Features.Playlists.Commands;
using Rythm.Application.Features.Playlists.DTOs;
using Rythm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Mappings
{
    public class PlaylistProfile : Profile
    {
        public PlaylistProfile()
        {
            CreateMap<Playlist, ResultPlaylistDto>();

            CreateMap<Playlist, GetPlaylistByIdDto>()
                 .ForMember(dest => dest.Songs, opt => opt.MapFrom(src => src.PlaylistSongs.Select(ps => ps.Song)));

            CreateMap<CreatePlaylistCommand, Playlist>();
            CreateMap<UpdatePlaylistCommand, Playlist>();
        }
    }
}
