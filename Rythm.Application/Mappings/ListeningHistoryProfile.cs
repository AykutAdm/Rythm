using AutoMapper;
using Rythm.Application.Features.ListeningHistories.Commands;
using Rythm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Mappings
{
    public class ListeningHistoryProfile : Profile
    {
        public ListeningHistoryProfile()
        {
            CreateMap<CreateListeningHistoryCommand, ListeningHistory>();
        }
    }
}
