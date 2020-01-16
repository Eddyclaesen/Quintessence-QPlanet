using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class CreateNewProjectRoleLanguageRequest
    {
        [DataMember]
        public Guid ProjectRoleId { get; set; }

        [DataMember]
        public int LanguageId { get; set; }
    }
}