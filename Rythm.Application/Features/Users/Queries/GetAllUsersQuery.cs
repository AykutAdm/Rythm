using MediatR;
using Rythm.Application.Features.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Users.Queries
{
    public class GetAllUsersQuery : IRequest<List<ResultUserDto>>
    {
    }
}
