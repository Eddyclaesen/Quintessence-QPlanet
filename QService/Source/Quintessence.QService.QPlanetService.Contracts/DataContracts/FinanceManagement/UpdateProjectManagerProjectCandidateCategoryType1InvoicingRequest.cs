using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.FinanceManagement
{
    [DataContract]
    public class UpdateProjectManagerProjectCandidateCategoryType1InvoicingRequest : UpdateInvoicingBaseRequest 
    {
        [DataMember]
        public string PurchaseOrderNumber { get; set; }
    }
}