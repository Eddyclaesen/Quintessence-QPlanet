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
    public class HasNeoPdfByCandidateIdQueryHandler : IRequestHandler<HasNeoPdfByCandidateIdQuery, bool>
    {
        private const string NeopirFolder = "Neopir";

        private readonly Settings _settings;

        public HasNeoPdfByCandidateIdQueryHandler(IOptionsMonitor<Settings> settings)
        {
            _settings = settings.CurrentValue;
        }

        public Task<bool> Handle(HasNeoPdfByCandidateIdQuery request, CancellationToken cancellationToken)
        {
            var filePath = FileLocationHelper.GetNeoFileLocation(Path.Combine(_settings.PdfStorageLocation, NeopirFolder), request.CandidateId);

            return Task.FromResult(File.Exists(filePath));

        }
    }
}