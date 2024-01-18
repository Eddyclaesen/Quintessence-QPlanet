using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class ListProductScoresResponse
    {
        [DataMember]
        public List<NeopirScoreView> NeopirScores { get; set; }

        [DataMember]
        public List<LeaderScoreView> LeaderScores { get; set; }

        [DataMember]
        public List<ProjectCandidateRoiScoreView> RoiScores { get; set; }

        [DataMember]
        public ProjectCandidateView ProjectCandidate { get; set; }

        [DataMember]
        public ProjectView Project { get; set; }

        [DataMember]
        public bool MotivationInterview { get; set; }
    }
}