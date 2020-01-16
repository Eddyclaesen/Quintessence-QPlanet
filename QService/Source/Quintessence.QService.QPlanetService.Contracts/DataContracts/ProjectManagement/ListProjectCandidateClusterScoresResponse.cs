using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class ListProjectCandidateClusterScoresResponse : ListProjectCandidateScoresResponse
    {
        [DataMember]
        public List<ProjectCandidateClusterScoreView> ProjectCandidateClusterScores { get; set; }
    }
}