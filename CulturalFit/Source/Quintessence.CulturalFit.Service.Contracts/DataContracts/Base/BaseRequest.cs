using System;
using System.Runtime.Serialization;

namespace Quintessence.CulturalFit.Service.Contracts.DataContracts.Base
{
    [DataContract]
    public abstract class BaseRequest
    {
        [DataMember]
        public int ContactId { get; set; }

        [DataMember]
        public int TheoremListRequestTypeId { get; set; }

        [DataMember]
        public DateTime Deadline { get; set; }
    }
}
