using System;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectPriceIndexRequest : UpdateRequestBase
    {
        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public decimal Index { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }
    }
}