﻿using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Dim;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectCandidateIndicatorScoreView : ViewEntityBase
    {
        [DataMember]
        public Guid ProjectCandidateId { get; set; }

        [DataMember]
        public ProjectCandidateView ProjectCandidate { get; set; }

        [DataMember]
        public Guid ProjectCandidateCompetenceScoreId { get; set; }

        [DataMember]
        public ProjectCandidateCompetenceScoreView ProjectCandidateCompetenceScore { get; set; }

        [DataMember]
        public Guid DictionaryIndicatorId { get; set; }

        [DataMember]
        public DictionaryIndicatorView DictionaryIndicator { get; set; }

        [DataMember]
        public decimal? Score { get; set; }

        [DataMember]
        public bool IsStandard { get; set; }

        [DataMember]
        public bool IsDistinctive { get; set; }
    }
}
