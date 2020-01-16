using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectCandidateResumeFieldView : ViewEntityBase
    {
        [DataMember]
        public Guid ProjectCandidateResumeId { get; set; }

        [DataMember]
        public Guid CandidateReportDefinitionFieldId { get; set; }

        [DataMember]
        public string CandidateReportDefinitionFieldName { get; set; }

        [DataMember]
        public string Statement { get; set; }

        [DataMember]
        public ProjectCandidateResumeView ProjectCandidateResume { get; set; }
    }
}