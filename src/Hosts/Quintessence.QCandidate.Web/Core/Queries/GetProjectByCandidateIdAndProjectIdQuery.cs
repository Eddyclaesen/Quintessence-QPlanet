using System;
using System.Collections.Generic;
using MediatR;
using Quintessence.QCandidate.Models.Assessments;

namespace Quintessence.QCandidate.Core.Queries
{
    public class GetProjectByCandidateIdAndProjectIdQuery : IRequest<Project>
    {
        public GetProjectByCandidateIdAndProjectIdQuery(Guid candidateId, Guid projectId)
        {
            CandidateId = candidateId;
            ProjectId = projectId;
        }

        public Guid CandidateId { get; }
        public Guid ProjectId { get; set; }
    }
}
