using System;
using MediatR;
using Quintessence.QCandidate.Core.Domain;

namespace Quintessence.QCandidate.Core.Commands
{
    public class CreateMemoProgramComponentCommand : IRequest<MemoProgramComponent>
    {
        public CreateMemoProgramComponentCommand(Guid id, Guid userId, Guid simulationCombinationId)
        {
            Id = id;
            UserId = userId;
            SimulationCombinationId = simulationCombinationId;
        }

        public Guid Id { get; }
        public Guid UserId { get; }
        public Guid SimulationCombinationId { get; }
    }
}