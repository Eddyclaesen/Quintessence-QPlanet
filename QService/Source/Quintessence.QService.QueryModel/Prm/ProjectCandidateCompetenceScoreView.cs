using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Dim;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectCandidateCompetenceScoreView : ViewEntityBase
    {
        [DataMember]
        public Guid ProjectCandidateId { get; set; }

        [DataMember]
        public ProjectCandidateView ProjectCandidate { get; set; }

        [DataMember]
        public Guid? ProjectCandidateClusterScoreId { get; set; }

        [DataMember]
        public ProjectCandidateClusterScoreView ProjectCandidateClusterScore { get; set; }

        [DataMember]
        public Guid DictionaryCompetenceId { get; set; }

        [DataMember]
        public DictionaryCompetenceView DictionaryCompetence { get; set; }

        [DataMember]
        public decimal? Score { get; set; }

        [DataMember]
        public string Statement { get; set; }

        [DataMember]
        public List<ProjectCandidateIndicatorScoreView> ProjectCandidateIndicatorScores { get; set; }
    }
}