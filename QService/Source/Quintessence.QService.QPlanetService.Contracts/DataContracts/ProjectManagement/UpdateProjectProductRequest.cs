using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;
using System;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectProductRequest : UpdateRequestBase
    {
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