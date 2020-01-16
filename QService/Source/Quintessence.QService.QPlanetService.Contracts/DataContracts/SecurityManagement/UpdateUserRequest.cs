using System;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SecurityManagement
{
    [DataContract]
    [KnownType(typeof(UpdateEmployeeRequest))]
    public class UpdateUserRequest : UpdateRequestBase
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public string ResetPassword { get; set; }

        [DataMember]
        public bool IsEmployee { get; set; }

        [DataMember]
        public bool IsLocked { get; set; }

        [DataMember]
        public Guid? RoleId { get; set; }
    }
}
