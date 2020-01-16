using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.FinanceManagement
{
    [KnownType(typeof(ListProjectManagerInvoicingRequest))]
    [KnownType(typeof(ListCustomerAssistantInvoicingRequest))]
    [KnownType(typeof(ListAccountantInvoicingRequest))]
    [DataContract]
    public class ListInvoicingBaseRequest
    {
        [DataMember]
        public DateTime Date { get; set; }
    }
}