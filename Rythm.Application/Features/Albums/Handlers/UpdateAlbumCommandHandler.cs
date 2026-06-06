using AutoMapper;
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
    public class UpdateAlbumCommandHandler : IRequestHandler<UpdateAlbumCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAlbumCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdateAlbumCommand request, CancellationToken cancellationToken)
        {
            var value = await _unitOfWork.Albums.GetByIdAsync(request.AlbumId);
            _mapper.Map(request, value);
            await _unitOfWork.Albums.UpdateAsync(value);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
