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
                                
                var memos = simulationCombinationMemos.Select(scm => new Memo(scm.Position, scm.Id)).ToList();
                                
                var predecesorCalendarDays = await _mediator.Send(new GetPredecessorCalendarDaysBySimulationIdAndUserIdQuery(request.SimulationCombinationId, request.UserId));

                // No predecessor exists
                if (!predecesorCalendarDays.Any())
                {
                    memoProgramComponent = _repository.Add(new MemoProgramComponent(
                        request.Id,
                        request.SimulationCombinationId,
                        request.UserId,
                        memos
                    ));
                }
                else
                {
                    var highestMemoPosition = simulationCombinationMemos.Max(scm => scm.Position);
                    var predecessorMemos = (await _mediator.Send(new GetPredecessorMemosBySimulationCombinationIdAndUserIdQuery(request.SimulationCombinationId, request.UserId)));

                    memos.AddRange(predecessorMemos.Select(m => new Memo(m.Position + highestMemoPosition, m.OriginId)));

                    memoProgramComponent = _repository.Add(new MemoProgramComponent(
                        request.Id,
                        request.SimulationCombinationId,
                        request.UserId,
                        memos,
                        predecesorCalendarDays.Select(cd => new CalendarDay(cd.Day, cd.Note))
                    ));
                }
             

                await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            }

            return memoProgramComponent;
        }
    }
}