using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.QueryModel.Fin;

namespace Quintessence.QService.Business.QueryRepositories
{
    public class FinanceManagementQueryRepository : QueryRepositoryBase<IFinQueryContext>,
                                                    IFinanceManagementQueryRepository
    {
        public FinanceManagementQueryRepository(IUnityContainer unityContainer)
            : base(unityContainer)
        {
        }

        public List<InvoicingBaseEntryView> ListCustomerAssistantInvoicingEntries(DateTime date, Guid? customerAssistantId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = new List<InvoicingBaseEntryView>();
                        entries.AddRange(context.ListInvoicingProjectCandidateCategoryType1Entries(date, customerAssistantId).ToList());
                        entries.AddRange(context.ListInvoicingProjectCandidateCategoryType2Entries(date, customerAssistantId).ToList());
                        entries.AddRange(context.ListInvoicingProjectCandidateCategoryType3Entries(date, customerAssistantId).ToList());
                        entries.AddRange(context.ListInvoicingProjectCandidateEntries(date, customerAssistantId).ToList());
                        entries.AddRange(context.ListInvoicingProjectProductEntries(date, customerAssistantId).ToList());
                        entries.AddRange(context.ListInvoicingAssessmentDevelopmentProjectFixedPriceEntries(date, customerAssistantId).ToList());
                        entries.AddRange(context.ListInvoicingConsultancyProjectFixedPriceEntries(date, customerAssistantId).ToList());
                        entries.AddRange(context.ListInvoicingProductSheetEntries(date, customerAssistantId).ToList());
                        entries.AddRange(context.ListInvoicingTimesheetEntries(date, customerAssistantId).ToList());

                        return entries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<InvoicingBaseEntryView> ListProjectManagerInvoicingEntries(DateTime date, Guid projectManagerId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = new List<InvoicingBaseEntryView>();
                        entries.AddRange(context.ListInvoicingProjectCandidateCategoryType1Entries(date, null, projectManagerId).ToList());
                        entries.AddRange(context.ListInvoicingProjectCandidateCategoryType2Entries(date, null, projectManagerId).ToList());
                        entries.AddRange(context.ListInvoicingProjectCandidateCategoryType3Entries(date, null, projectManagerId).ToList());
                        entries.AddRange(context.ListInvoicingProjectCandidateEntries(date, null, projectManagerId).ToList());
                        entries.AddRange(context.ListInvoicingProjectProductEntries(date, null, projectManagerId).ToList());
                        entries.AddRange(context.ListInvoicingAssessmentDevelopmentProjectFixedPriceEntries(date, null, projectManagerId).ToList());
                        entries.AddRange(context.ListInvoicingConsultancyProjectFixedPriceEntries(date, null, projectManagerId).ToList());
                        entries.AddRange(context.ListInvoicingProductSheetEntries(date, null, projectManagerId).ToList());
                        entries.AddRange(context.ListInvoicingTimesheetEntries(date, null, projectManagerId).ToList());

                        return entries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public List<AccountantInvoicingBaseEntryView> ListAccountantInvoicingEntries(DateTime date)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = new List<AccountantInvoicingBaseEntryView>();
                        entries.AddRange(context.ListAccountantInvoicingProjectCandidateCategoryType1Entries(date).ToList());
                        entries.AddRange(context.ListAccountantInvoicingProjectCandidateCategoryType2Entries(date).ToList());
                        entries.AddRange(context.ListAccountantInvoicingProjectCandidateCategoryType3Entries(date).ToList());
                        entries.AddRange(context.ListAccountantInvoicingProjectCandidateEntries(date).ToList());
                        entries.AddRange(context.ListAccountantInvoicingProjectProductEntries(date).ToList());
                        entries.AddRange(context.ListAccountantInvoicingAcdcProjectFixedPriceEntries(date).ToList());
                        entries.AddRange(context.ListAccountantInvoicingConsultancyProjectFixedPriceEntries(date).ToList());
                        entries.AddRange(context.ListAccountantInvoicingProductSheetEntries(date).ToList());
                        entries.AddRange(context.ListAccountantInvoicingTimesheetEntries(date).ToList());

                        return entries;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public AccountantInvoicingProjectCandidateEntryView RetrieveAccountantProjectCandidateInvoicingEntry(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.ListAccountantInvoicingProjectCandidateEntries(id:id).ToList();
                        return entries.FirstOrDefault();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public AccountantInvoicingProjectCandidateCategoryType1EntryView RetrieveAccountantProjectCandidateCategoryType1InvoicingEntry(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.ListAccountantInvoicingProjectCandidateCategoryType1Entries(id: id).ToList();
                        return entries.FirstOrDefault();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public AccountantInvoicingProjectCandidateCategoryType2EntryView RetrieveAccountantProjectCandidateCategoryType2InvoicingEntry(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.ListAccountantInvoicingProjectCandidateCategoryType2Entries(id: id).ToList();
                        return entries.FirstOrDefault();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public AccountantInvoicingProjectCandidateCategoryType3EntryView RetrieveAccountantProjectCandidateCategoryType3InvoicingEntry(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.ListAccountantInvoicingProjectCandidateCategoryType3Entries(id: id).ToList();
                        return entries.FirstOrDefault();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public AccountantInvoicingProjectProductEntryView RetrieveAccountantProjectProductInvoicingEntry(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.ListAccountantInvoicingProjectProductEntries(id: id).ToList();
                        return entries.FirstOrDefault();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public AccountantInvoicingAcdcProjectFixedPriceEntryView RetrieveAccountantAcdcProjectFixedPriceInvoicingEntry(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.ListAccountantInvoicingAcdcProjectFixedPriceEntries(id: id).ToList();
                        return entries.FirstOrDefault();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public AccountantInvoicingConsultancyProjectFixedPriceEntryView RetrieveAccountantConsultancyProjectFixedPriceInvoicingEntry(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.ListAccountantInvoicingConsultancyProjectFixedPriceEntries(id: id).ToList();
                        return entries.FirstOrDefault();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public AccountantInvoicingProductSheetEntryView RetrieveAccountantProductSheetEntryInvoicingEntry(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.ListAccountantInvoicingProductSheetEntries(id: id).ToList();
                        return entries.FirstOrDefault();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }

        public AccountantInvoicingTimesheetEntryView RetrieveAccountantTimesheetEntryInvoicingEntry(Guid id)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var entries = context.ListAccountantInvoicingTimesheetEntries(id: id).ToList();
                        return entries.FirstOrDefault();
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }
    }
}