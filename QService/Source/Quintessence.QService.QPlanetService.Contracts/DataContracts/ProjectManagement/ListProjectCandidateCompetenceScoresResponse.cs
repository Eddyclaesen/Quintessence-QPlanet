using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class ListProjectCandidateCompetenceScoresResponse : ListProjectCandidateScoresResponse
    {
        [DataMember]
        public List<ProjectCandidateCompetenceScoreView> ProjectCandidateCompetenceScores { get; set; }
    }
}