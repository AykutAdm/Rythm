using MediatR;
using Rythm.Application.Features.Users.Commands;
using Rythm.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Users.Handlers
{
    public class UnlikeSongCommandHandler : IRequestHandler<UnlikeSongCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnlikeSongCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UnlikeSongCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.LikedSongs.RemoveAsync(request.AppUserId, request.SongId);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
