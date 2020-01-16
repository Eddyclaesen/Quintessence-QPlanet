using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.FinanceManagement
{
    [DataContract]
    public class ListCustomerAssistantInvoicingRequest : ListInvoicingBaseRequest
    {
        [DataMember]
        public Guid? CustomerAssistantId { get; set; }
    }
}