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
            var memoProgram = _repository.FindAsync(request.MemoProgramComponentId).Result;
            foreach (var resultMemo in memoProgram.Memos)
            {
                if (request.MemoPositions.ContainsKey(resultMemo.Id))
                {
                    resultMemo.Update(request.MemoPositions[resultMemo.Id]);
                }
                    
            }
            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return memoProgram;

        }
    }
}
