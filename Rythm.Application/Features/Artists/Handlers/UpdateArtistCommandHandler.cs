using AutoMapper;
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
    public class UpdateArtistCommandHandler : IRequestHandler<UpdateArtistCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateArtistCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdateArtistCommand request, CancellationToken cancellationToken)
        {
            var value = await _unitOfWork.Artists.GetByIdAsync(request.ArtistId);
            _mapper.Map(request, value);
            await _unitOfWork.Artists.UpdateAsync(value);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
