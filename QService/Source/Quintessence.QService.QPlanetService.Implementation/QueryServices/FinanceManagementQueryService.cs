using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.FinanceManagement;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base;
using Quintessence.QService.QueryModel.Fin;

namespace Quintessence.QService.QPlanetService.Implementation.QueryServices
{
    public class FinanceManagementQueryService : SecuredUnityServiceBase, IFinanceManagementQueryService
    {
        #region List
        public List<InvoicingBaseEntryView> ListCustomerAssistantInvoicingEntries(ListCustomerAssistantInvoicingRequest request)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IFinanceManagementQueryRepository>();

                return repository.ListCustomerAssistantInvoicingEntries(request.Date, request.CustomerAssistantId);
            });
        }

        public List<InvoicingBaseEntryView> ListProjectManagerInvoicingEntries(ListProjectManagerInvoicingRequest request)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IFinanceManagementQueryRepository>();

                return repository.ListProjectManagerInvoicingEntries(request.Date, request.ProjectManagerId);
            });
        }

        public List<AccountantInvoicingBaseEntryView> ListAccountantInvoicingEntries(ListAccountantInvoicingRequest request)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IFinanceManagementQueryRepository>();

                return repository.ListAccountantInvoicingEntries(request.Date);
            });
        }
        #endregion

        #region Retrieve
        public AccountantInvoicingProjectCandidateEntryView RetrieveAccountantProjectCandidateInvoicingEntry(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IFinanceManagementQueryRepository>();

                var entry = repository.RetrieveAccountantProjectCandidateInvoicingEntry(id);

                return entry;
            });
        }

        public AccountantInvoicingProjectCandidateCategoryType1EntryView RetrieveAccountantProjectCandidateCategoryType1InvoicingEntry(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IFinanceManagementQueryRepository>();

                var entry = repository.RetrieveAccountantProjectCandidateCategoryType1InvoicingEntry(id);

                return entry;
            });
        }

        public AccountantInvoicingProjectCandidateCategoryType2EntryView RetrieveAccountantProjectCandidateCategoryType2InvoicingEntry(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IFinanceManagementQueryRepository>();

                var entry = repository.RetrieveAccountantProjectCandidateCategoryType2InvoicingEntry(id);

                return entry;
            });
        }

        public AccountantInvoicingProjectCandidateCategoryType3EntryView RetrieveAccountantProjectCandidateCategoryType3InvoicingEntry(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IFinanceManagementQueryRepository>();

                var entry = repository.RetrieveAccountantProjectCandidateCategoryType3InvoicingEntry(id);

                return entry;
            });
        }

        public AccountantInvoicingProjectProductEntryView RetrieveAccountantProjectProductInvoicingEntry(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IFinanceManagementQueryRepository>();

                var entry = repository.RetrieveAccountantProjectProductInvoicingEntry(id);

                return entry;
            });
        }

        public AccountantInvoicingAcdcProjectFixedPriceEntryView RetrieveAccountantAcdcProjectFixedPriceInvoicingEntry(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IFinanceManagementQueryRepository>();

                var entry = repository.RetrieveAccountantAcdcProjectFixedPriceInvoicingEntry(id);

                return entry;
            });
        }

        public AccountantInvoicingConsultancyProjectFixedPriceEntryView RetrieveAccountantConsultancyProjectFixedPriceInvoicingEntry(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IFinanceManagementQueryRepository>();

                var entry = repository.RetrieveAccountantConsultancyProjectFixedPriceInvoicingEntry(id);

                return entry;
            });
        }

        public AccountantInvoicingProductSheetEntryView RetrieveAccountantProductSheetEntryInvoicingEntry(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IFinanceManagementQueryRepository>();

                var entry = repository.RetrieveAccountantProductSheetEntryInvoicingEntry(id);

                return entry;
            });
        }

        public AccountantInvoicingTimesheetEntryView RetrieveAccountantTimesheetEntryInvoicingEntry(Guid id)
        {
            LogTrace();

            return Execute(() =>
            {
                var repository = Container.Resolve<IFinanceManagementQueryRepository>();

                var entry = repository.RetrieveAccountantTimesheetEntryInvoicingEntry(id);

                return entry;
            });
        }

        #endregion
        
    }
}