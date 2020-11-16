using System;
using MediatR;
using Quintessence.QCandidate.Core.Domain;

namespace Quintessence.QCandidate.Core.Commands
{
    public class CreateMemoProgramComponentIfNotExistsCommand : IRequest<MemoProgramComponent>
    {
        public CreateMemoProgramComponentIfNotExistsCommand(Guid id, Guid userId, Guid simulationCombinationId, Guid? predecessorId)
        {
            Id = id;
            UserId = userId;
            SimulationCombinationId = simulationCombinationId;
            PredecessorId = predecessorId;
        }

        public Guid Id { get; }
        public Guid UserId { get; }
        public Guid SimulationCombinationId { get; }
        public Guid? PredecessorId { get; }
    }
}