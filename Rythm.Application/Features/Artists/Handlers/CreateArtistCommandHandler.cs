using AutoMapper;
using MediatR;
using Rythm.Application.Features.Artists.Commands;
using Rythm.Application.Interfaces;
using Rythm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Artists.Handlers
{
    public class CreateArtistCommandHandler : IRequestHandler<CreateArtistCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public CreateArtistCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task Handle(CreateArtistCommand request, CancellationToken cancellationToken)
        {
            var value = _mapper.Map<Artist>(request);
            value.CreatedAt = DateTime.Now;
            await _unitOfWork.Artists.AddAsync(value);
            await _unitOfWork.SaveChangesAsync();

            // New artist added, clear cache
            await _cacheService.RemoveAsync("artists_all");
        }
    }
}
