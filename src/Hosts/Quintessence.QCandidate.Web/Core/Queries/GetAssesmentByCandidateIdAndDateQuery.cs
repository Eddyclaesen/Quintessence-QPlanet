using System;
using MediatR;
using Quintessence.QCandidate.Contracts.Responses;

namespace Quintessence.QCandidate.Core.Queries
{
    public class GetAssesmentByCandidateIdAndDateQuery : IRequest<AssessmentDto>
    {
        public GetAssesmentByCandidateIdAndDateQuery(Guid candidateId, DateTime date)
        {
            CandidateId = candidateId;
            Date = date;
        }

        public Guid CandidateId { get; }
        public DateTime Date { get; }
    }
}