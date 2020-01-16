using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class CreateProjectTypeCategoryUnitPriceRequest
    {
        [DataMember]
        public Guid ProjectTypeCategoryId { get; set; }

        [DataMember]
        public Guid ProjectTypeCategoryLevelId { get; set; }

        [DataMember]
        public decimal UnitPrice { get; set; }
    }
}