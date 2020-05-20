using MediatR;
using Quintessence.QCandidate.Core.Queries;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Quintessence.QCandidate.Helpers;
using Quintessence.QCandidate.Models;

namespace Quintessence.QCandidate.Logic.Queries
{
    public class GetSimulationCombinationPdfByIdAndLanguageQueryHandler : IRequestHandler<GetSimulationCombinationPdfByIdAndLanguageQuery, FileStream>
    {
        private readonly Settings _settings;

        public GetSimulationCombinationPdfByIdAndLanguageQueryHandler(IOptionsMonitor<Settings> settings)
        {
            _settings = settings.CurrentValue;
        }

        public Task<FileStream> Handle(GetSimulationCombinationPdfByIdAndLanguageQuery request, CancellationToken cancellationToken)
        {
            var filename = FileLocationHelper.GetPdfFileLocation(_settings.PdfStorageLocation, request.SimulationCombinationId, request.Language);

            if(File.Exists(filename))
            {
                return Task.FromResult(new FileStream(filename, FileMode.Open));
            }

            return Task.FromResult<FileStream>(null);
        }
    }
}