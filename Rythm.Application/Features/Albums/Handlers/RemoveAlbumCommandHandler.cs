using MediatR;
using Rythm.Application.Features.Albums.Commands;
using Rythm.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Albums.Handlers
{
    public class RemoveAlbumCommandHandler : IRequestHandler<RemoveAlbumCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveAlbumCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RemoveAlbumCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Albums.DeleteAsync(request.Id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
