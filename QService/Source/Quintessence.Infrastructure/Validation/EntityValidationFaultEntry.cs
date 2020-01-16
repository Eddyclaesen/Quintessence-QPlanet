using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Quintessence.Infrastructure.Validation
{
    [DataContract]
    public class EntityValidationFaultEntry : FaultEntry
    {
        [DataMember]
        public List<string> MemberNames { get; set; }
    }
}