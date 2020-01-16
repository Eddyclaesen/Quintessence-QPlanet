using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Sec
{
    [DataContract(IsReference = true)]
    public class UserGroupView
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}