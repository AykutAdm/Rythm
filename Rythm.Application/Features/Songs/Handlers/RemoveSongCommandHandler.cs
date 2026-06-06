using MediatR;
using Rythm.Application.Features.Songs.Commands;
using Rythm.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Songs.Handlers
{
    public class RemoveSongCommandHandler : IRequestHandler<RemoveSongCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveSongCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RemoveSongCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Songs.DeleteAsync(request.Id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
