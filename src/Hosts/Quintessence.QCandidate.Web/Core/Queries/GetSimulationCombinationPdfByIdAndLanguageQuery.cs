using System;
using System.IO;
using MediatR;

namespace Quintessence.QCandidate.Core.Queries
{
    public class GetSimulationCombinationPdfByIdAndLanguageQuery : IRequest<FileStream>
    {
        public GetSimulationCombinationPdfByIdAndLanguageQuery(Guid simulationCombinationId, string language)
        {
            SimulationCombinationId = simulationCombinationId;
            Language = language;
        }

        public Guid SimulationCombinationId { get; }
        public string Language { get; }
    }
}