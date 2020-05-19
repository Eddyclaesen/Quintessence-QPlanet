using MediatR;
using Quintessence.QCandidate.Core.Queries;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Quintessence.QCandidate.Logic.Queries
{
    public class GetSimulationCombinationPdfByIdAndLanguageQueryHandler : IRequestHandler<GetSimulationCombinationPdfByIdAndLanguageQuery, FileStream>
    {
        public Task<FileStream> Handle(GetSimulationCombinationPdfByIdAndLanguageQuery request, CancellationToken cancellationToken)
        {
            var filename = GetPdfFileLocation(request.PdfStorageLocation, request.SimulationCombinationId, request.Language);

            if(File.Exists(filename))
            {
                return Task.FromResult(new FileStream(filename, FileMode.Open));
            }

            return Task.FromResult<FileStream>(null);
        }

        private string GetPdfFileLocation(string pdfStorageLocation, Guid simulationCombinationId, string language)
        {
            var filename = $"{simulationCombinationId}.pdf";

            return Path.Combine(pdfStorageLocation, language, filename);
        }
    }
}