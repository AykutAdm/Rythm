using MediatR;
using Rythm.Application.Features.Users.Commands;
using Rythm.Application.Interfaces;
using Rythm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Users.Handlers
{
    public class LikeSongCommandHandler : IRequestHandler<LikeSongCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LikeSongCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(LikeSongCommand request, CancellationToken cancellationToken)
        {
            var isLiked = await _unitOfWork.LikedSongs.IsLikedAsync(request.AppUserId, request.SongId);

            var likedSong = new UserLikedSong
            {
                AppUserId = request.AppUserId,
                SongId = request.SongId,
                LikedAt = DateTime.Now
            };
            await _unitOfWork.LikedSongs.AddAsync(likedSong);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
