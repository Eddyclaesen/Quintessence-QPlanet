using System.IO;
using MediatR;
using Quintessence.QCandidate.Core.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Quintessence.QCandidate.Logic.Queries
{
    public class HasSimulationCombinationPdfByIdAndLanguageQueryHandler : IRequestHandler<HasSimulationCombinationPdfByIdAndLanguageQuery, bool>
    {
        public Task<bool> Handle(HasSimulationCombinationPdfByIdAndLanguageQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(File.Exists(request.Filename));
        }
    }
}