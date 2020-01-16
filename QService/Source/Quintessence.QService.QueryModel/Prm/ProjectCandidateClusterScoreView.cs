using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Dim;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectCandidateClusterScoreView : ViewEntityBase
    {
        [DataMember]
        public Guid ProjectCandidateId { get; set; }

        [DataMember]
        public ProjectCandidateView ProjectCandidate { get; set; }

        [DataMember]
        public Guid DictionaryClusterId { get; set; }

        [DataMember]
        public DictionaryClusterView DictionaryCluster { get; set; }

        [DataMember]
        public decimal? Score { get; set; }

        [DataMember]
        public string Statement { get; set; }

        [DataMember]
        public List<ProjectCandidateCompetenceScoreView> ProjectCandidateCompetenceScores { get; set; }
    }
}