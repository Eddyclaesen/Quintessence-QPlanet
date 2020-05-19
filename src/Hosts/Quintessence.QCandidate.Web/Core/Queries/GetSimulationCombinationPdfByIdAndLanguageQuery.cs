using System;
using System.IO;
using MediatR;

namespace Quintessence.QCandidate.Core.Queries
{
    public class GetSimulationCombinationPdfByIdAndLanguageQuery : IRequest<FileStream>
    {
        public GetSimulationCombinationPdfByIdAndLanguageQuery(string pdfStorageLocation, Guid simulationCombinationId, string language)
        {
            PdfStorageLocation = pdfStorageLocation;
            SimulationCombinationId = simulationCombinationId;
            Language = language;
        }

        public string PdfStorageLocation { get; }
        public Guid SimulationCombinationId { get; }
        public string Language { get; }
    }
}