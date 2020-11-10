using MediatR;
using Quintessence.QCandidate.Core.Commands;
using Quintessence.QCandidate.Core.Domain;
using Quintessence.QCandidate.Infrastructure.EntityFrameworkCore.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Quintessence.QCandidate.Logic.Commands
{
    public class CreateMemoProgramComponentCommandHandler : IRequestHandler<CreateMemoProgramComponentCommand, MemoProgramComponent>
    {
        private readonly IMemoProgramComponentRepository _repository;

        public CreateMemoProgramComponentCommandHandler(IMemoProgramComponentRepository repository)
        {
            _repository = repository;
        }

        public async Task<MemoProgramComponent> Handle(CreateMemoProgramComponentCommand request, CancellationToken cancellationToken)
        {
            var memoProgramComponent = new MemoProgramComponent(request.SimulationCombinationId, request.UserId, request.Memos, request.CalendarDays);
            
            _repository.Add(memoProgramComponent);

            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return memoProgramComponent;
        }
    }
}