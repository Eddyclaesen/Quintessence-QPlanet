using System;
using MediatR;
using Quintessence.QCandidate.Contracts.Responses;

namespace Quintessence.QCandidate.Core.Queries
{
    public class GetMemoProgramComponentBySimulationCombinationIdAndLanguageQuery : IRequest<MemoProgramComponentDto>
    {
        public GetMemoProgramComponentBySimulationCombinationIdAndLanguageQuery(Guid id, string language)
        {
            Id = id;
            Language = language;
        }

        public Guid Id { get; }
        public string Language { get; }
    }
}