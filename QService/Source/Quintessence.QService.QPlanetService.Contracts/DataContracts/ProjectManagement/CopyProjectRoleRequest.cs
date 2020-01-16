using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class CopyProjectRoleRequest
    {
        [DataMember]
        public Guid ProjectRoleToCopyId { get; set; }

        [DataMember]
        public int? ContactId { get; set; }
    }
}
