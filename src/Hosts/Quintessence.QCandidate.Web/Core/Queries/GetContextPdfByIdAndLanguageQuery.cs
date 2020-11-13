using System;
using System.IO;
using MediatR;

namespace Quintessence.QCandidate.Core.Queries
{
    public class GetContextPdfByIdAndLanguageQuery : IRequest<FileStream>
    {
        public GetContextPdfByIdAndLanguageQuery(Guid id, string language)
        {
            Id = id;
            Language = language;
        }

        public Guid Id { get; }
        public string Language { get; }
    }
}