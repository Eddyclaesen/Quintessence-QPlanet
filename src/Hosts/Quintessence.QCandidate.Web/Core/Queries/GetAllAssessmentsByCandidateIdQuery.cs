using System;
using System.Collections.Generic;
using MediatR;
using Quintessence.QCandidate.Models.Assessments;

namespace Quintessence.QCandidate.Core.Queries
{
    public class GetAllAssessmentsByCandidateIdQuery : IRequest<IEnumerable<AllAssessments>>
    {
        public GetAllAssessmentsByCandidateIdQuery(Guid candidateId)
        {
            CandidateId = candidateId;
        }

        public Guid CandidateId { get; }
    }
}
