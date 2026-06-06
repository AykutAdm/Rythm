using MediatR;
using Rythm.Application.Features.Artists.Commands;
using Rythm.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Artists.Handlers
{
    public class RemoveArtistCommandHandler : IRequestHandler<RemoveArtistCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveArtistCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RemoveArtistCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Artists.DeleteAsync(request.Id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
