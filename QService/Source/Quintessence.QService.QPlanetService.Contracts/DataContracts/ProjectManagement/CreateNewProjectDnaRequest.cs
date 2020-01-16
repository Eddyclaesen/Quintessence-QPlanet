using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class CreateNewProjectDnaRequest
    {
        [DataMember]
        public int CrmProjectId { get; set; }

        [DataMember]
        public bool ApprovedForReferences { get; set; }
    }
}