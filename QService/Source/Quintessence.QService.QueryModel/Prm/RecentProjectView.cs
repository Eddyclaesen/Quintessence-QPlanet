using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract]
    public class RecentProjectView
    {
        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public string ProjectName { get; set; }

        [DataMember]
        public string ContactName { get; set; }

        [DataMember]
        public string ProjectTypeName { get; set; }
    }
}
