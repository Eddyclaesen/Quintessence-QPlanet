using System;
using MediatR;
using Quintessence.QCandidate.Core.Domain;
using MemoProgramComponent = Quintessence.QCandidate.Models.MemoProgramComponents.MemoProgramComponent;

namespace Quintessence.QCandidate.Core.Queries
{
    public class GetMemoProgramComponentByIdAndLanguageQuery : IRequest<MemoProgramComponent>
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