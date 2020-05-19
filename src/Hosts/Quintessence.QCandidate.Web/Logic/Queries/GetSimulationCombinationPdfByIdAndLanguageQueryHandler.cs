using MediatR;
using Quintessence.QCandidate.Core.Queries;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Quintessence.QCandidate.Helpers;

namespace Quintessence.QCandidate.Logic.Queries
{
    public class GetSimulationCombinationPdfByIdAndLanguageQueryHandler : IRequestHandler<GetSimulationCombinationPdfByIdAndLanguageQuery, FileStream>
    {
        public Task<FileStream> Handle(GetSimulationCombinationPdfByIdAndLanguageQuery request, CancellationToken cancellationToken)
        {
            var filename = FileLocationHelper.GetPdfFileLocation(request.PdfStorageLocation, request.SimulationCombinationId, request.Language);

            if(File.Exists(filename))
            {
                return Task.FromResult(new FileStream(filename, FileMode.Open));
            }

            return Task.FromResult<FileStream>(null);
        }
    }
}