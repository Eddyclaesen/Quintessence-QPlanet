using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class ListProjectCandidateStandardSimulationScoresResponse : ListProjectCandidateSimulationScoresResponse
    {
        [DataMember]
        public List<ProjectCandidateIndicatorSimulationScoreView> ProjectCandidateIndicatorSimulationScores { get; set; }
    }
}