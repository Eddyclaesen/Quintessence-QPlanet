using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared
{
    [DataContract]
    public class UpdateRequestBase
    {
        [DataMember(IsRequired = true)]
        public Guid Id { get; set; }

        [DataMember(IsRequired = true)]
        public Guid AuditVersionid { get; set; }
    }
}
