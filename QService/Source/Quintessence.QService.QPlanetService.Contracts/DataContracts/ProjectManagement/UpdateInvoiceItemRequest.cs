using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateInvoiceItemRequest : UpdateRequestBase
    {
        [DataMember]
        public bool IsMainCategory { get; set; }

        [DataMember]
        public int InvoiceStatusCode { get; set; }

        [DataMember]
        public decimal InvoiceAmount { get; set; }
    }
}