using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.FinanceManagement
{
    [DataContract]
    public class UpdateCustomerAssistantProductSheetEntryInvoicingRequest : UpdateInvoicingBaseRequest
    {
        [DataMember]
        public string PurchaseOrderNumber { get; set; }
    }
}