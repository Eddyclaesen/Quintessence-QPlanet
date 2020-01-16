using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class CreateNewProjectPriceIndexRequest
    {
        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public decimal Index { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }
    }
}
