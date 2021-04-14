using System;
using MediatR;

namespace Quintessence.QCandidate.Core.Queries
{
    public class HasNeoPdfByCandidateIdQuery : IRequest<bool>
    {
        public HasNeoPdfByCandidateIdQuery(Guid candidateId)
        {
            CandidateId = candidateId;
        }

        public Guid CandidateId { get; }
    }
}