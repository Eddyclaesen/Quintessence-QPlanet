using System;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectTypeCategoryUnitPriceRequest : UpdateRequestBase
    {
        [DataMember]
        public decimal? UnitPrice { get; set; }

        [DataMember]
        public Guid ProjectTypeCategoryId { get; set; }

        [DataMember]
        public Guid ProjectTypeCategoryLevelId { get; set; }
    }
}