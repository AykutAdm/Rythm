using MediatR;
using Rythm.Application.Features.Dashboard.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Dashboard.Queries
{
    public class GetDashboardStatsQuery : IRequest<DashboardStatsDto>
    {
    }
}
