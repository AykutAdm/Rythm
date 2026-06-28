using AutoMapper;
using MediatR;
using Rythm.Application.Features.ListeningHistories.Commands;
using Rythm.Application.Interfaces;
using Rythm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.ListeningHistories.Handlers
{
    public class CreateListeningHistoryCommandHandler : IRequestHandler<CreateListeningHistoryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateListeningHistoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(CreateListeningHistoryCommand request, CancellationToken cancellationToken)
        {
            var value = _mapper.Map<ListeningHistory>(request);
            value.ListenedAt = DateTime.Now;
            await _unitOfWork.ListeningHistories.AddAsync(value);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
