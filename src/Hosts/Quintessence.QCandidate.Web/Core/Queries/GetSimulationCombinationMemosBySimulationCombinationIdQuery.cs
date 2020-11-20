using System;
using System.Collections.Generic;
using MediatR;
using Quintessence.QCandidate.Core.Domain;

namespace Quintessence.QCandidate.Core.Queries
{
    public class GetSimulationCombinationMemosBySimulationCombinationIdQuery : IRequest<IEnumerable<SimulationCombinationMemo>>
    {
        public GetSimulationCombinationMemosBySimulationCombinationIdQuery(Guid simulationCombinationId)
        {
            SimulationCombinationId = simulationCombinationId;
        }

        public Guid SimulationCombinationId { get; }
        
    }
}