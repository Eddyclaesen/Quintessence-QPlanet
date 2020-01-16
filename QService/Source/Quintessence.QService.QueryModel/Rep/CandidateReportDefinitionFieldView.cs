using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Rep
{
    [DataContract(IsReference = true)]
    public class CandidateReportDefinitionFieldView : ViewEntityBase
    {
        [DataMember]
        public Guid CandidateReportDefinitionId { get; set; }

        [DataMember]
        public CandidateReportDefinitionView CandidateReportDefinition { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public bool IsActive { get; set; }			
    }
}