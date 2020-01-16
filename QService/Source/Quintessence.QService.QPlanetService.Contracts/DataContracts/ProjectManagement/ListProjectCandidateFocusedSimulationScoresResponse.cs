using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class ListProjectCandidateFocusedSimulationScoresResponse : ListProjectCandidateSimulationScoresResponse
    {
        [DataMember]
        public List<ProjectCandidateIndicatorSimulationFocusedScoreView> ProjectCandidateIndicatorSimulationFocusedScores { get; set; }
    }
}