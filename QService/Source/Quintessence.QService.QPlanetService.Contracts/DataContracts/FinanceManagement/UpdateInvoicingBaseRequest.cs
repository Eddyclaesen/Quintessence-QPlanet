using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.FinanceManagement
{
    [KnownType(typeof(UpdateProjectManagerTimesheetEntryInvoicingRequest))]
    [KnownType(typeof(UpdateProjectManagerProductSheetEntryInvoicingRequest))]
    [KnownType(typeof(UpdateProjectManagerProjectCandidateInvoicingRequest))]
    [KnownType(typeof(UpdateProjectManagerProjectCandidateCategoryType1InvoicingRequest))]
    [KnownType(typeof(UpdateProjectManagerProjectCandidateCategoryType2InvoicingRequest))]
    [KnownType(typeof(UpdateProjectManagerProjectCandidateCategoryType3InvoicingRequest))]
    [KnownType(typeof(UpdateProjectManagerProjectProductInvoicingRequest))]
    [KnownType(typeof(UpdateProjectManagerAcdcProjectFixedPriceInvoicingRequest))]
    [KnownType(typeof(UpdateProjectManagerConsultancyProjectFixedPriceInvoicingRequest))]
    [KnownType(typeof(UpdateCustomerAssistantTimesheetEntryInvoicingRequest))]
    [KnownType(typeof(UpdateCustomerAssistantProductSheetEntryInvoicingRequest))]
    [KnownType(typeof(UpdateCustomerAssistantProjectCandidateInvoicingRequest))]
    [KnownType(typeof(UpdateCustomerAssistantProjectCandidateCategoryType1InvoicingRequest))]
    [KnownType(typeof(UpdateCustomerAssistantProjectCandidateCategoryType2InvoicingRequest))]
    [KnownType(typeof(UpdateCustomerAssistantProjectCandidateCategoryType3InvoicingRequest))]
    [KnownType(typeof(UpdateCustomerAssistantProjectProductInvoicingRequest))]
    [KnownType(typeof(UpdateCustomerAssistantAcdcProjectFixedPriceInvoicingRequest))]
    [KnownType(typeof(UpdateCustomerAssistantConsultancyProjectFixedPriceInvoicingRequest))]
    [KnownType(typeof(UpdateAccountantInvoicingBaseRequest))]
    [DataContract]
    public class UpdateInvoicingBaseRequest : UpdateRequestBase
    {
        [DataMember]
        public string InvoiceRemarks { get; set; }

        [DataMember]
        public int InvoiceStatusCode { get; set; }

        [DataMember]
        public decimal? InvoiceAmount { get; set; }

        [DataMember]
        public string BceEntity { get; set; }
    }
}