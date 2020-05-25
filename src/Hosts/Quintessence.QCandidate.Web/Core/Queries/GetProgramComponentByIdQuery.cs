using System;
using MediatR;
using Quintessence.QCandidate.Contracts.Responses;

namespace Quintessence.QCandidate.Core.Queries
{
    public class GetProgramComponentByIdQuery : IRequest<ProgramComponentDto>
    {
        public GetProgramComponentByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}