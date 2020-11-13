using System;
using MediatR;
using Quintessence.QCandidate.Contracts.Responses;
using Quintessence.QCandidate.Core.Domain;

namespace Quintessence.QCandidate.Core.Queries
{
    public class GetMemoProgramComponentByIdAndLanguageQuery : IRequest<MemoProgramComponentDto>
    {
        public GetMemoProgramComponentByIdAndLanguageQuery(Guid id, Language language)
        {
            Id = id;
            Language = language;
        }

        public Guid Id { get; }
        public Language Language { get; }
    }
}