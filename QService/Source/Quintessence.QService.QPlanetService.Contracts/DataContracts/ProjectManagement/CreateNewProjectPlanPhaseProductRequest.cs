using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class CreateNewProjectPlanPhaseProductRequest : CreateNewProjectPlanPhaseEntryRequest
    {
        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public Guid ProductId { get; set; }

        [DataMember]
        public String ProductName { get; set; }

        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public decimal UnitPrice { get; set; }

        [DataMember]
        public decimal TotalPrice { get; set; }

        [DataMember]
        public bool NoInvoice { get; set; }

        [DataMember]
        public Guid UserId { get; set; }
    }


}