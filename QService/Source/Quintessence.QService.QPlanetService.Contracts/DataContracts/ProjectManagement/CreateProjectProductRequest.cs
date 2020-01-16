using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class CreateProjectProductRequest
    {
        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public Guid ProductTypeId { get; set; }

        [DataMember]
        public decimal? InvoiceAmount { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public DateTime? Deadline { get; set; }

        [DataMember]
        public bool NoInvoice { get; set; }
    }
}