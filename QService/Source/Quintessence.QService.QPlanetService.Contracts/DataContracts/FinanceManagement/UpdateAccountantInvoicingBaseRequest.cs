using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.FinanceManagement
{
    [KnownType(typeof(UpdateAccountantTimesheetEntryInvoicingRequest))]
    [KnownType(typeof(UpdateAccountantProductSheetEntryInvoicingRequest))]
    [KnownType(typeof(UpdateAccountantProjectCandidateInvoicingRequest))]
    [KnownType(typeof(UpdateAccountantProjectCandidateCategoryType1InvoicingRequest))]
    [KnownType(typeof(UpdateAccountantProjectCandidateCategoryType2InvoicingRequest))]
    [KnownType(typeof(UpdateAccountantProjectCandidateCategoryType3InvoicingRequest))]
    [KnownType(typeof(UpdateAccountantProjectProductInvoicingRequest))]
    [KnownType(typeof(UpdateAccountantAcdcProjectFixedPriceInvoicingRequest))]
    [KnownType(typeof(UpdateAccountantConsultancyProjectFixedPriceInvoicingRequest))]
    [DataContract]
    public class UpdateAccountantInvoicingBaseRequest : UpdateInvoicingBaseRequest
    {
        [DataMember]
        public string InvoiceNumber { get; set; }

        [DataMember]
        public DateTime? InvoicedDate { get; set; }

    }
}