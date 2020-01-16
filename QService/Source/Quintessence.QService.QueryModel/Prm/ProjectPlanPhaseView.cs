using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectPlanPhaseView : ViewEntityBase
    {
        [DataMember]
        public Guid ProjectPlanId { get; set; }

        [DataMember]
        public ProjectPlanView ProjectPlan { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }

        [DataMember]
        public List<ProjectPlanPhaseEntryView> ProjectPlanPhaseEntries { get; set; }
    }
}
