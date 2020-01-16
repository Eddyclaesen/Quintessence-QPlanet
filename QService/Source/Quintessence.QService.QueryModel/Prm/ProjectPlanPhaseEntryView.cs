using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(ProjectPlanPhaseActivityView))]
    [KnownType(typeof(ProjectPlanPhaseProductView))]
    public class ProjectPlanPhaseEntryView : ViewEntityBase
    {
        [DataMember]
        public Guid ProjectPlanPhaseId { get; set; }
        
        [DataMember]
        public ProjectPlanPhaseView ProjectPlanPhase { get; set; }

        [DataMember]
        public decimal Quantity { get; set; }

        [DataMember]
        public DateTime Deadline { get; set; }
    }
}
