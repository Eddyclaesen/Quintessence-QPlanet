using MediatR;
using Microsoft.Extensions.Options;
using Quintessence.QCandidate.Configuration;
using Quintessence.QCandidate.Core.Queries;
using Quintessence.QCandidate.Helpers;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Quintessence.QCandidate.Logic.Queries
{
    public class HasSimulationCombinationPdfByIdAndLanguageQueryHandler : IRequestHandler<HasSimulationCombinationPdfByIdAndLanguageQuery, bool>
    {
        private readonly Settings _settings;

        public HasSimulationCombinationPdfByIdAndLanguageQueryHandler(IOptionsMonitor<Settings> settings)
        {
            _settings = settings.CurrentValue;
        }

        public Task<bool> Handle(HasSimulationCombinationPdfByIdAndLanguageQuery request, CancellationToken cancellationToken)
        {
            var filePath = FileLocationHelper.GetPdfFileLocation(_settings.PdfStorageLocation, request.SimulationCombinationId, request.Language);

            return Task.FromResult(File.Exists(filePath));

        }
    }
}