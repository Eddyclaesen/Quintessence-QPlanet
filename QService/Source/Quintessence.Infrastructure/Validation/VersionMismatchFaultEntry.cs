using System;
using System.Runtime.Serialization;

namespace Quintessence.Infrastructure.Validation
{
    [DataContract]
    public class VersionMismatchFaultEntry : FaultEntry
    {
        [DataMember]
        public string UpdateObject { get; set; }

        [DataMember]
        public string StoreObject { get; set; }

        [DataMember]
        public string ModifiedBy { get; set; }

        [DataMember]
        public DateTime? ModifiedOn { get; set; }

        public override string Message
        {
            get { return string.Format(base.Message, ModifiedBy, ModifiedOn.GetValueOrDefault()); }
        }
    }
}
