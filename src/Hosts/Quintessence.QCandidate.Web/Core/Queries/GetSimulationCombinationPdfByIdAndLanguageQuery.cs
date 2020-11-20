using System;
using System.IO;
using MediatR;

namespace Quintessence.QCandidate.Core.Queries
{
    public class GetSimulationCombinationPdfByIdAndLanguageQuery : IRequest<FileStream>
    {
        public GetSimulationCombinationPdfByIdAndLanguageQuery(Guid id, string language)
        {
            Id = id;
            Language = language;
        }

        public Guid Id { get; }
        public string Language { get; }
    }
}