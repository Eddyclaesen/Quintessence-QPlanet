using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectRevenueDistributionView : ViewEntityBase
    {
        [DataMember]
        public Guid ProjectId { get; set; }
        
        [DataMember]
        public ProjectView Project { get; set; }

        [DataMember]
        public int CrmProjectId { get; set; }

        [DataMember]
        public decimal? Revenue { get; set; }
    }
}