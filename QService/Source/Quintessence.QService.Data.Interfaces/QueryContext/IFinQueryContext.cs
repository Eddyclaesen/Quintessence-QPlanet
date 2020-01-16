using System;
using System.Collections.Generic;
using Quintessence.QService.QueryModel.Fin;

namespace Quintessence.QService.Data.Interfaces.QueryContext
{
    public interface IFinQueryContext : IQuintessenceQueryContext
    {
        #region Customer assistant and project manager invoicing
        IEnumerable<InvoicingProjectCandidateEntryView> ListInvoicingProjectCandidateEntries(DateTime date, Guid? customerAssistantId = null, Guid? projectManagerId = null);
        IEnumerable<InvoicingProjectProductEntryView> ListInvoicingProjectProductEntries(DateTime date, Guid? customerAssistantId = null, Guid? projectManagerId = null);
        IEnumerable<InvoicingProjectCandidateCategoryType1EntryView> ListInvoicingProjectCandidateCategoryType1Entries(DateTime date,Guid? customerAssistantId = null, Guid? projectManagerId = null);
        IEnumerable<InvoicingProjectCandidateCategoryType2EntryView> ListInvoicingProjectCandidateCategoryType2Entries(DateTime date, Guid? customerAssistantId = null, Guid? projectManagerId = null);
        IEnumerable<InvoicingProjectCandidateCategoryType3EntryView> ListInvoicingProjectCandidateCategoryType3Entries(DateTime date, Guid? customerAssistantId = null, Guid? projectManagerId = null);
        IEnumerable<InvoicingAssessmentDevelopmentProjectFixedPriceEntryView> ListInvoicingAssessmentDevelopmentProjectFixedPriceEntries(DateTime date, Guid? customerAssistantId = null, Guid? projectManagerId = null);
        IEnumerable<InvoicingConsultancyProjectFixedPriceEntryView> ListInvoicingConsultancyProjectFixedPriceEntries(DateTime date, Guid? customerAssistantId = null, Guid? projectManagerId = null);
        IEnumerable<InvoicingProductSheetEntryView> ListInvoicingProductSheetEntries(DateTime date, Guid? customerAssistantId = null, Guid? projectManagerId = null);
        IEnumerable<InvoicingTimesheetEntryView> ListInvoicingTimesheetEntries(DateTime date, Guid? customerAssistantId = null, Guid? projectManagerId = null);
        #endregion

        #region Accountant invoicing
        IEnumerable<AccountantInvoicingProjectCandidateEntryView> ListAccountantInvoicingProjectCandidateEntries(DateTime? date = null, Guid? id = null);
        IEnumerable<AccountantInvoicingProjectProductEntryView> ListAccountantInvoicingProjectProductEntries(DateTime? date = null, Guid? id = null);
        IEnumerable<AccountantInvoicingProjectCandidateCategoryType1EntryView> ListAccountantInvoicingProjectCandidateCategoryType1Entries(DateTime? date = null, Guid? id = null);
        IEnumerable<AccountantInvoicingProjectCandidateCategoryType2EntryView> ListAccountantInvoicingProjectCandidateCategoryType2Entries(DateTime? date = null, Guid? id = null);
        IEnumerable<AccountantInvoicingProjectCandidateCategoryType3EntryView> ListAccountantInvoicingProjectCandidateCategoryType3Entries(DateTime? date = null, Guid? id = null);
        IEnumerable<AccountantInvoicingAcdcProjectFixedPriceEntryView> ListAccountantInvoicingAcdcProjectFixedPriceEntries(DateTime? date = null, Guid? id = null);
        IEnumerable<AccountantInvoicingConsultancyProjectFixedPriceEntryView> ListAccountantInvoicingConsultancyProjectFixedPriceEntries(DateTime? date = null, Guid? id = null);
        IEnumerable<AccountantInvoicingProductSheetEntryView> ListAccountantInvoicingProductSheetEntries(DateTime? date = null, Guid? id = null);
        IEnumerable<AccountantInvoicingTimesheetEntryView> ListAccountantInvoicingTimesheetEntries(DateTime? date = null, Guid? id = null);
        #endregion

    }
}