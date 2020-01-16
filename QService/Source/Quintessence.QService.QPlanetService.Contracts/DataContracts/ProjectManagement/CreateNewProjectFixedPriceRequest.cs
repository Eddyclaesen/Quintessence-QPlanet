using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class CreateNewProjectFixedPriceRequest
    {
        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public DateTime Deadline { get; set; }

        [DataMember]
        public string InvoiceRemarks { get; set; }
    }
}