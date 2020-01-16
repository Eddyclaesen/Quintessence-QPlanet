using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    [KnownType(typeof(ListProjectCandidateFocusedSimulationScoresResponse))]
    [KnownType(typeof(ListProjectCandidateStandardSimulationScoresResponse))]
    public class ListProjectCandidateSimulationScoresResponse
    {
        [DataMember]
        public ProjectCandidateView ProjectCandidate { get; set; }

        [DataMember]
        public List<ProjectCandidateCompetenceSimulationScoreView> ProjectCandidateCompetenceSimulationScores { get; set; }
    }
}