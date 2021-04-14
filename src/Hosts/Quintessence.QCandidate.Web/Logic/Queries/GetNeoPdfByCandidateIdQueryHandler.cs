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
    public class GetNeoPdfByCandidateIdQueryHandler : IRequestHandler<GetNeoPdfByCandidateIdQuery, FileStream>
    {
        private const string NeopirFolder = "Neopir";

        private readonly Settings _settings;

        public GetNeoPdfByCandidateIdQueryHandler(IOptionsMonitor<Settings> settings)
        {
            _settings = settings.CurrentValue;
        }

        public Task<FileStream> Handle(GetNeoPdfByCandidateIdQuery request, CancellationToken cancellationToken)
        {
            var filename = FileLocationHelper.GetNeoFileLocation(Path.Combine(_settings.PdfStorageLocation, NeopirFolder), request.Id);

            if (File.Exists(filename))
            {
                return Task.FromResult(new FileStream(filename, FileMode.Open));
            }

            return Task.FromResult<FileStream>(null);
        }
    }
}