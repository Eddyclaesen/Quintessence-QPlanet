using MediatR;
using Quintessence.QCandidate.Core.Commands;
using Quintessence.QCandidate.Core.Domain;
using Quintessence.QCandidate.Infrastructure.EntityFrameworkCore.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Quintessence.QCandidate.Logic.Commands
{
    public class ChangeMemosPositionCommandHandler : IRequestHandler<ChangeMemosPositionCommand, MemoProgramComponent>
    {
        private readonly IMemoProgramComponentRepository _repository;

        public ChangeMemosPositionCommandHandler(IMemoProgramComponentRepository repository)
        {
            _repository = repository;
        }

        public async Task<MemoProgramComponent> Handle(ChangeMemosPositionCommand request, CancellationToken cancellationToken)
        {
            var memoProgramComponent = _repository.FindAsync(request.MemoProgramComponentId).Result;
            foreach (var memoPosition in request.MemoPositions)
            {
                memoProgramComponent.UpdateMemo(memoPosition.Key, memoPosition.Value);
            }
            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return memoProgramComponent;

        }
    }
}
