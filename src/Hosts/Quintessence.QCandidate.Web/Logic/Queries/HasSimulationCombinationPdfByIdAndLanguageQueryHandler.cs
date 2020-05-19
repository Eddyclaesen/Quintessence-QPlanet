using System.IO;
using MediatR;
using Quintessence.QCandidate.Core.Queries;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Quintessence.QCandidate.Helpers;

namespace Quintessence.QCandidate.Logic.Queries
{
    public class HasSimulationCombinationPdfByIdAndLanguageQueryHandler : IRequestHandler<HasSimulationCombinationPdfByIdAndLanguageQuery, bool>
    {
        private readonly string _pdfStorageLocation;
        public HasSimulationCombinationPdfByIdAndLanguageQueryHandler(IConfiguration configuration)
        {
            _pdfStorageLocation = configuration.GetValue<string>("PdfStorageLocation");
        }

        public Task<bool> Handle(HasSimulationCombinationPdfByIdAndLanguageQuery request, CancellationToken cancellationToken)
        {
            var filePath = FileLocationHelper.GetPdfFileLocation(_pdfStorageLocation, request.SimulationCombinationId, request.Language);
            
            return Task.FromResult(File.Exists(filePath));
        }
    }
}