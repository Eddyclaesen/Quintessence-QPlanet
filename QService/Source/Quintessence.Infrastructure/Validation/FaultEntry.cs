using System.Runtime.Serialization;

namespace Quintessence.Infrastructure.Validation
{
    [DataContract]
    public class FaultEntry
    {
        [DataMember]
        public virtual string Message { get; set; }
    }
}
