using System;
using System.Collections.Generic;
using MediatR;
using Quintessence.QCandidate.Core.Domain;

namespace Quintessence.QCandidate.Core.Commands
{
    public class ChangeMemosPositionCommand : IRequest<MemoProgramComponent>
    {
        public ChangeMemosPositionCommand(Guid memoProgramComponentId, Dictionary<Guid, int> memoPositions)
        {
            MemoProgramComponentId = memoProgramComponentId;
            MemoPositions = memoPositions;
        }

        public Guid MemoProgramComponentId { get; }
        public Dictionary<Guid,int> MemoPositions { get; }
        
    }
}