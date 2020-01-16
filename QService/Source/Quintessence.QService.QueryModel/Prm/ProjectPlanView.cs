using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectPlanView : ViewEntityBase
    {
        [DataMember]
        public List<ProjectPlanPhaseView> ProjectPlanPhases { get; set; }
    }
}