using AutoMapper;
using Rythm.Application.Features.Genres.Commands;
using Rythm.Application.Features.Genres.DTOs;
using Rythm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Mappings
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, ResultGenreDto>();

            CreateMap<Genre, GetGenreByIdDto>()
                .ForMember(dest => dest.Songs, opt => opt.MapFrom(src => src.Songs));

            CreateMap<CreateGenreCommand, Genre>();
            CreateMap<UpdateGenreCommand, Genre>();
        }
    }
}
