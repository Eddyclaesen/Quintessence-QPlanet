using System;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateCulturalFitContactRequestRequest : UpdateRequestBase
    {
        [DataMember]
        public int ContactId { get; set; }

        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public int CrmEmailId { get; set; }

        [DataMember]
        public DateTime Deadline { get; set; }

        [DataMember]
        public string Description { get; set; }

    }
}