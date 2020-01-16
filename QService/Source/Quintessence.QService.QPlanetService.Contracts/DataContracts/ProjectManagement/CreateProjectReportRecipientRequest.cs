using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class CreateProjectReportRecipientRequest
    {
        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public int CrmEmailId { get; set; }
    }
}