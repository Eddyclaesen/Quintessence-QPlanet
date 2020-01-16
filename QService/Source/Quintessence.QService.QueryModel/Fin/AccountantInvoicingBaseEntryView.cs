using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Fin
{
    [KnownType(typeof(AccountantInvoicingProductSheetEntryView))]
    [KnownType(typeof(AccountantInvoicingTimesheetEntryView))]
    [KnownType(typeof(AccountantInvoicingAcdcProjectFixedPriceEntryView))]
    [KnownType(typeof(AccountantInvoicingConsultancyProjectFixedPriceEntryView))]
    [KnownType(typeof(AccountantInvoicingProjectProductEntryView))]
    [KnownType(typeof(AccountantInvoicingProjectCandidateCategoryType1EntryView))]
    [KnownType(typeof(AccountantInvoicingProjectCandidateCategoryType2EntryView))]
    [KnownType(typeof(AccountantInvoicingProjectCandidateCategoryType3EntryView))]
    [KnownType(typeof(AccountantInvoicingProjectCandidateEntryView))]
    [DataContract(IsReference = true)]
    public class AccountantInvoicingBaseEntryView : InvoicingBaseEntryView
    {
        [DataMember]
        public string CrmProjectName { get; set; }
    }
}