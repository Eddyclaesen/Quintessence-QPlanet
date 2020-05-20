using System.IO;
using MediatR;
using Quintessence.QCandidate.Core.Queries;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Quintessence.QCandidate.Helpers;
using Quintessence.QCandidate.Models;

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