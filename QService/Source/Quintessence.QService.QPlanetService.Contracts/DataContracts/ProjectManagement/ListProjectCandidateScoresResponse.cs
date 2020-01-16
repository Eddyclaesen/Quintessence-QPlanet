using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Rep;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    [KnownType(typeof(ListProjectCandidateClusterScoresResponse))]
    [KnownType(typeof(ListProjectCandidateCompetenceScoresResponse))]
    public class ListProjectCandidateScoresResponse
    {
        [DataMember]
        public ProjectCandidateView ProjectCandidate { get; set; }

        [DataMember]
        public AssessmentDevelopmentProjectView Project { get; set; }

        [DataMember]
        public List<CandidateScoreReportTypeView> ReportTypes { get; set; }

        [DataMember]
        public ListProjectCandidateSimulationScoresResponse ProjectCandidateSimulationScores { get; set; }

        [DataMember]
        public List<ProjectCandidateIndicatorSimulationScoreView> ProjectCandidateIndicatorSimulationScores { get; set; }

        [DataMember]
        public List<ProjectCandidateIndicatorSimulationFocusedScoreView> ProjectCandidateIndicatorSimulationFocusedScores { get; set; }

        [DataMember]
        public List<ProjectCandidateIndicatorScoreView> ProjectCandidateIndicatorScores { get; set; }

        [DataMember]
        public List<ProjectCandidateCompetenceSimulationScoreView> ProjectCandidateCompetenceSimulationScores { get; set; }
    }
}