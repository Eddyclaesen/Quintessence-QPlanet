using System;
using System.Collections.Generic;
using Quintessence.QService.QueryModel.Fin;

namespace Quintessence.QService.Business.Interfaces.QueryRepositories
{
    public interface IFinanceManagementQueryRepository : IQueryRepository
    {
        #region List
        List<InvoicingBaseEntryView> ListCustomerAssistantInvoicingEntries(DateTime date, Guid? customerAssistantId);
        List<InvoicingBaseEntryView> ListProjectManagerInvoicingEntries(DateTime date, Guid projectManagerId);
        List<AccountantInvoicingBaseEntryView> ListAccountantInvoicingEntries(DateTime date);
        #endregion

        #region Retrieve
        AccountantInvoicingProjectCandidateEntryView RetrieveAccountantProjectCandidateInvoicingEntry(Guid id);
        AccountantInvoicingProjectCandidateCategoryType1EntryView RetrieveAccountantProjectCandidateCategoryType1InvoicingEntry(Guid id);
        AccountantInvoicingProjectCandidateCategoryType2EntryView RetrieveAccountantProjectCandidateCategoryType2InvoicingEntry(Guid id);
        AccountantInvoicingProjectCandidateCategoryType3EntryView RetrieveAccountantProjectCandidateCategoryType3InvoicingEntry(Guid id);
        AccountantInvoicingProjectProductEntryView RetrieveAccountantProjectProductInvoicingEntry(Guid id);
        AccountantInvoicingAcdcProjectFixedPriceEntryView RetrieveAccountantAcdcProjectFixedPriceInvoicingEntry(Guid id);
        AccountantInvoicingConsultancyProjectFixedPriceEntryView RetrieveAccountantConsultancyProjectFixedPriceInvoicingEntry(Guid id);
        AccountantInvoicingProductSheetEntryView RetrieveAccountantProductSheetEntryInvoicingEntry(Guid id);
        AccountantInvoicingTimesheetEntryView RetrieveAccountantTimesheetEntryInvoicingEntry(Guid id);
        #endregion

    }
}