using System.Runtime.Serialization;

namespace Quintessence.Infrastructure.Validation
{
    [DataContract]
    public class ValidationFaultEntry : FaultEntry
    {
        [DataMember]
        public string Code { get; set; }
    }
}