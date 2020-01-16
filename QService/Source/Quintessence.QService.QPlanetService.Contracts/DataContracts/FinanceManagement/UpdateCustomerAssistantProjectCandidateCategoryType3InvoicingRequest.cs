using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.FinanceManagement
{
    [DataContract]
    public class UpdateCustomerAssistantProjectCandidateCategoryType3InvoicingRequest : UpdateInvoicingBaseRequest
    {
        [DataMember]
        public string PurchaseOrderNumber { get; set; }
    }
}