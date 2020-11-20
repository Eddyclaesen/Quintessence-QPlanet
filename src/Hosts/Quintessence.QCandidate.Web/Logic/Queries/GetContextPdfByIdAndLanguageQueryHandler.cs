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
    public class GetContextPdfByIdAndLanguageQueryHandler : IRequestHandler<GetContextPdfByIdAndLanguageQuery, FileStream>
    {
        private const string ContextsFolder = "Contexts";
        
        private readonly Settings _settings;

        public GetContextPdfByIdAndLanguageQueryHandler(IOptionsMonitor<Settings> settings)
        {
            _settings = settings.CurrentValue;
        }

        public Task<FileStream> Handle(GetContextPdfByIdAndLanguageQuery request, CancellationToken cancellationToken)
        {
            var filename = FileLocationHelper.GetPdfFileLocation(Path.Combine(_settings.PdfStorageLocation, ContextsFolder), request.Id, request.Language);

            if (File.Exists(filename))
            {
                return Task.FromResult(new FileStream(filename, FileMode.Open));
            }

            return Task.FromResult<FileStream>(null);
        }
    }
}