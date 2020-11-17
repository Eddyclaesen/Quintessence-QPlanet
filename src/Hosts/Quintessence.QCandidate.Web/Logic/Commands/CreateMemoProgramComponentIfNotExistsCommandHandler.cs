using System;
using MediatR;
using Quintessence.QCandidate.Core.Commands;
using Quintessence.QCandidate.Core.Domain;
using Quintessence.QCandidate.Core.Queries;
using Quintessence.QCandidate.Infrastructure.EntityFrameworkCore.Commands;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Quintessence.QCandidate.Logic.Commands
{
    public class CreateMemoProgramComponentIfNotExistsCommandHandler : IRequestHandler<CreateMemoProgramComponentIfNotExistsCommand, MemoProgramComponent>
    {
        private readonly IMemoProgramComponentRepository _repository;
        private readonly IMediator _mediator;

        public CreateMemoProgramComponentIfNotExistsCommandHandler(IMemoProgramComponentRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<MemoProgramComponent> Handle(CreateMemoProgramComponentIfNotExistsCommand request, CancellationToken cancellationToken)
        {
            var memoProgramComponent = await _repository.FindAsync(request.Id);

            if (memoProgramComponent == null)
            {
                var simulationCombinationMemos = await _mediator.Send(new GetSimulationCombinationMemosBySimulationCombinationIdQuery(request.SimulationCombinationId));

                var predecesorCalendarDays = await _mediator.Send(new GetPredecessorCalendarDaysBySimulationIdAndUserIdQuery(request.Id, request.UserId));

                memoProgramComponent = _repository.Add(new MemoProgramComponent(
                    request.Id,
                    request.SimulationCombinationId,
                    request.UserId,
                    simulationCombinationMemos.Select(scm => new Memo(scm.Position, scm.Id)),
                    predecesorCalendarDays
                ));

                var predecessorSimulationCombinationMemos = await _mediator.Send(new GetPredecessorMemosBySimulationCombinationIdAndUserIdQuery(request.SimulationCombinationId, request.UserId));
                if (predecessorSimulationCombinationMemos.Any())
                {
                    memoProgramComponent.AddPredecessorMemos(predecessorSimulationCombinationMemos);
                }

                await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            }

            return memoProgramComponent;
        }
    }
}