using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectCandidateResumeView : ViewEntityBase
    {
        [DataMember]
        public Guid ProjectCandidateId { get; set; }

        [DataMember]
        public int AdviceId { get; set; }

        [DataMember]
        public AdviceView Advice { get; set; }

        [DataMember]
        public string Reasoning { get; set; }

        [DataMember]
        public List<ProjectCandidateResumeFieldView> ProjectCandidateResumeFields { get; set; }
    }
}
