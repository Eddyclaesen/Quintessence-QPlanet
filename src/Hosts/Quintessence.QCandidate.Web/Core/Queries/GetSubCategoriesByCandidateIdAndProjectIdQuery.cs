using System;
using System.Collections.Generic;
using MediatR;
using Quintessence.QCandidate.Models.Assessments;

namespace Quintessence.QCandidate.Core.Queries
{
    public class GetSubCategoriesByCandidateIdAndProjectIdQuery : IRequest<IEnumerable<SubCategories>>
    {
        public GetSubCategoriesByCandidateIdAndProjectIdQuery(Guid candidateId, Guid projectId)
        {
            CandidateId = candidateId;
            ProjectId = projectId;
        }

        public Guid CandidateId { get; }

        public Guid ProjectId { get; set; }
    }
}
