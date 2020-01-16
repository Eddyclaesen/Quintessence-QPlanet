using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ConsultancyProjectView : ProjectView
    {
        [DataMember]
        public Guid ProjectPlanId { get; set; }

        [DataMember]
        public ProjectPlanView ProjectPlan { get; set; }

        public ProjectStatusCodeViewType Status { get { return (ProjectStatusCodeViewType)StatusCode; } }

        [DataMember]
        public string ProjectInformation { get; set; }
    }
}
