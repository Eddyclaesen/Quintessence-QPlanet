using System;
using System.Collections.Generic;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.QueryModel.Fin;

namespace Quintessence.QService.Data.QueryContext
{
    /// <summary>
    /// Quintessence data context
    /// </summary>
    public partial class QuintessenceQueryContext : IFinQueryContext
    {
        #region Customer assistant and project manager invoicing
        public IEnumerable<InvoicingProjectCandidateEntryView> ListInvoicingProjectCandidateEntries(DateTime date, Guid? customerAssistantId = null, Guid? projectManagerId = null)
        {
            var query = Database.SqlQuery<InvoicingProjectCandidateEntryView>("Invoicing_ProjectCandidate {0}, {1}, {2}", date, customerAssistantId, projectManagerId);
            return query;
        }

        public IEnumerable<InvoicingProjectProductEntryView> ListInvoicingProjectProductEntries(DateTime date, Guid? customerAssistantId = null, Guid? projectManagerId = null)
        {
            var query = Database.SqlQuery<InvoicingProjectProductEntryView>("Invoicing_ProjectProduct {0}, {1}, {2}", date, customerAssistantId, projectManagerId);
            return query;
        }

        public IEnumerable<InvoicingProjectCandidateCategoryType1EntryView> ListInvoicingProjectCandidateCategoryType1Entries(DateTime date, Guid? customerAssistantId = null,
                                                                             Guid? projectManagerId = null)
        {
            var query = Database.SqlQuery<InvoicingProjectCandidateCategoryType1EntryView>("Invoicing_ProjectCandidateCategoryType1 {0}, {1}, {2}", date, customerAssistantId, projectManagerId);
            return query;
        }

        public IEnumerable<InvoicingProjectCandidateCategoryType2EntryView> ListInvoicingProjectCandidateCategoryType2Entries(DateTime date, Guid? customerAssistantId = null, Guid? projectManagerId = null)
        {
            var query = Database.SqlQuery<InvoicingProjectCandidateCategoryType2EntryView>("Invoicing_ProjectCandidateCategoryType2 {0}, {1}, {2}", date, customerAssistantId, projectManagerId);
            return query;
        }

        public IEnumerable<InvoicingProjectCandidateCategoryType3EntryView> ListInvoicingProjectCandidateCategoryType3Entries(DateTime date, Guid? customerAssistantId = null,
                                                                             Guid? projectManagerId = null)
        {
            var query = Database.SqlQuery<InvoicingProjectCandidateCategoryType3EntryView>("Invoicing_ProjectCandidateCategoryType3 {0}, {1}, {2}", date, customerAssistantId, projectManagerId);
            return query;
        }

        public IEnumerable<InvoicingAssessmentDevelopmentProjectFixedPriceEntryView> ListInvoicingAssessmentDevelopmentProjectFixedPriceEntries(DateTime date, Guid? customerAssistantId = null, Guid? projectManagerId = null)
        {
            var query = Database.SqlQuery<InvoicingAssessmentDevelopmentProjectFixedPriceEntryView>("Invoicing_AssessmentDevelopmentProjectFixedPrice {0}, {1}, {2}", date, customerAssistantId, projectManagerId);
            return query;
        }

        public IEnumerable<InvoicingConsultancyProjectFixedPriceEntryView> ListInvoicingConsultancyProjectFixedPriceEntries(DateTime date, Guid? customerAssistantId = null,
                                                                            Guid? projectManagerId = null)
        {
            var query = Database.SqlQuery<InvoicingConsultancyProjectFixedPriceEntryView>("Invoicing_ConsultancyProjectFixedPrice {0}, {1}, {2}", date, customerAssistantId, projectManagerId);
            return query;
        }

        public IEnumerable<InvoicingProductSheetEntryView> ListInvoicingProductSheetEntries(DateTime date, Guid? customerAssistantId = null, Guid? projectManagerId = null)
        {
            var query = Database.SqlQuery<InvoicingProductSheetEntryView>("Invoicing_ProductSheetEntry {0}, {1}, {2}", date, customerAssistantId, projectManagerId);
            return query;
        }

        public IEnumerable<InvoicingTimesheetEntryView> ListInvoicingTimesheetEntries(DateTime date, Guid? customerAssistantId = null, Guid? projectManagerId = null)
        {
            var query = Database.SqlQuery<InvoicingTimesheetEntryView>("Invoicing_TimesheetEntry {0}, {1}, {2}", date, customerAssistantId, projectManagerId);
            return query;
        }

        #endregion

        #region Accountant invoicing
        public IEnumerable<AccountantInvoicingProjectCandidateEntryView> ListAccountantInvoicingProjectCandidateEntries(DateTime? date = null, Guid? id = null){
            var query = Database.SqlQuery<AccountantInvoicingProjectCandidateEntryView>("AccountantInvoicing_ProjectCandidate {0}, {1}", date, id);
            return query;
        }

        public IEnumerable<AccountantInvoicingProjectProductEntryView> ListAccountantInvoicingProjectProductEntries(DateTime? date = null, Guid? id = null)
        {
            var query = Database.SqlQuery<AccountantInvoicingProjectProductEntryView>("AccountantInvoicing_ProjectProduct {0}, {1}", date, id);
            return query;
        }

        public IEnumerable<AccountantInvoicingProjectCandidateCategoryType1EntryView> ListAccountantInvoicingProjectCandidateCategoryType1Entries(DateTime? date = null, Guid? id = null)
        {
            var query = Database.SqlQuery<AccountantInvoicingProjectCandidateCategoryType1EntryView>("AccountantInvoicing_ProjectCandidateCategoryType1 {0}, {1}", date, id);
            return query;
        }

        public IEnumerable<AccountantInvoicingProjectCandidateCategoryType2EntryView> ListAccountantInvoicingProjectCandidateCategoryType2Entries(DateTime? date = null, Guid? id = null)
        {
            var query = Database.SqlQuery<AccountantInvoicingProjectCandidateCategoryType2EntryView>("AccountantInvoicing_ProjectCandidateCategoryType2 {0}, {1}", date, id);
            return query;
        }

        public IEnumerable<AccountantInvoicingProjectCandidateCategoryType3EntryView> ListAccountantInvoicingProjectCandidateCategoryType3Entries(DateTime? date = null, Guid? id = null)
        {
            var query = Database.SqlQuery<AccountantInvoicingProjectCandidateCategoryType3EntryView>("AccountantInvoicing_ProjectCandidateCategoryType3 {0}, {1}", date, id);
            return query;
        }

        public IEnumerable<AccountantInvoicingAcdcProjectFixedPriceEntryView> ListAccountantInvoicingAcdcProjectFixedPriceEntries(DateTime? date = null, Guid? id = null){
            var query = Database.SqlQuery<AccountantInvoicingAcdcProjectFixedPriceEntryView>("AccountantInvoicing_AssessmentDevelopmentProjectFixedPrice {0}, {1}", date, id);
            return query;
        }

        public IEnumerable<AccountantInvoicingConsultancyProjectFixedPriceEntryView> ListAccountantInvoicingConsultancyProjectFixedPriceEntries(DateTime? date = null, Guid? id = null)
        {
            var query = Database.SqlQuery<AccountantInvoicingConsultancyProjectFixedPriceEntryView>("AccountantInvoicing_ConsultancyProjectFixedPrice {0}, {1}", date, id);
            return query;
        }

        public IEnumerable<AccountantInvoicingProductSheetEntryView> ListAccountantInvoicingProductSheetEntries(DateTime? date = null, Guid? id = null)
        {
            var query = Database.SqlQuery<AccountantInvoicingProductSheetEntryView>("AccountantInvoicing_ProductSheetEntry {0}, {1}", date, id);
            return query;
        }

        public IEnumerable<AccountantInvoicingTimesheetEntryView> ListAccountantInvoicingTimesheetEntries(DateTime? date = null, Guid? id = null)
        {
            var query = Database.SqlQuery<AccountantInvoicingTimesheetEntryView>("AccountantInvoicing_TimesheetEntry {0}, {1}", date, id);
            return query;
        }

        #endregion
    }
}
