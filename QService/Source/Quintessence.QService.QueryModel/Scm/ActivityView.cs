using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Scm
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(ActivityDetailView))]
    public class ActivityView : ViewEntityBase
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Guid ActivityTypeId { get; set; }

        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public string ActivityTypeName { get; set; }

        [DataMember]
        public List<ActivityProfileView> ActivityProfiles { get; set; }
    }
}