using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Scm
{
    [DataContract(IsReference = true)]
    public class ActivityTypeView : ViewEntityBase
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public bool IsSystem { get; set; }

        [DataMember]
        public List<ActivityTypeProfileView> ActivityTypeProfiles { get; set; }
    }
}