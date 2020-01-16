using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectUnitPricesRequest : UpdateRequestBase
    {
        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public List<UpdateUnitPriceRequest> UnitPriceRequests { get; set; }
    }
}