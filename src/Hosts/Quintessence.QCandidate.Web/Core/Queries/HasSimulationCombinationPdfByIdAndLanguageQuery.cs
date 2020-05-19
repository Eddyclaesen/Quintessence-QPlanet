using System;
using MediatR;

namespace Quintessence.QCandidate.Core.Queries
{
    public class HasSimulationCombinationPdfByIdAndLanguageQuery : IRequest<bool>
    {
        public HasSimulationCombinationPdfByIdAndLanguageQuery(Guid? simulationCombinationId, string language)
        {
            SimulationCombinationId = simulationCombinationId;
            Language = language;
        }

        public Guid? SimulationCombinationId { get; }
        public string Language { get; }
    }
}