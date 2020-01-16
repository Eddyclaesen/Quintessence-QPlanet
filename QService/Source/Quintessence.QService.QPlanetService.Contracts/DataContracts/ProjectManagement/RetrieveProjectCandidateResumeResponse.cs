using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class RetrieveProjectCandidateResumeResponse
    {
        [DataMember]
        public ProjectCandidateView ProjectCandidate { get; set; }

        [DataMember]
        public AssessmentDevelopmentProjectView Project { get; set; }

        [DataMember]
        public List<ProjectCandidateClusterScoreView> ProjectCandidateClusterScores { get; set; }

        [DataMember]
        public List<ProjectCandidateCompetenceScoreView> ProjectCandidateCompetenceScores { get; set; }

        [DataMember]
        public List<ProjectCandidateCompetenceSimulationScoreView> ProjectCandidateCompetenceSimulationScores { get; set; }

        [DataMember]
        public List<AdviceView> Advices { get; set; }

        [DataMember]
        public ProjectCandidateResumeView ProjectCandidateResume { get; set; }
    }
}