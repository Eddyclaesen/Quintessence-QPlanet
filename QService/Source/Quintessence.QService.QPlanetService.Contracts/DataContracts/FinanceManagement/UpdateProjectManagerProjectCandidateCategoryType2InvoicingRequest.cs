using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.FinanceManagement
{
    [DataContract]
    public class UpdateProjectManagerProjectCandidateCategoryType2InvoicingRequest : UpdateInvoicingBaseRequest 
    {
        [DataMember]
        public string PurchaseOrderNumber { get; set; }
    }
}