using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.WorkspaceManagement
{
    [DataContract]
    public class CreateNewUserProfileRequest
    {
        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public int LanguageId { get; set; }
    }
}