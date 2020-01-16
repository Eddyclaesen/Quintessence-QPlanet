using System;
using System.Collections.Generic;
using System.ServiceModel;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.FinanceManagement;
using Quintessence.QService.QueryModel.Fin;
using Quintessence.QService.QueryModel.Inf;

namespace Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts
{
    [ServiceContract]
    public interface IFinanceManagementQueryService
    {
        #region List
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<InvoicingBaseEntryView> ListCustomerAssistantInvoicingEntries(ListCustomerAssistantInvoicingRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<InvoicingBaseEntryView> ListProjectManagerInvoicingEntries(ListProjectManagerInvoicingRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<AccountantInvoicingBaseEntryView> ListAccountantInvoicingEntries(ListAccountantInvoicingRequest request);
        #endregion

        #region Retrieve
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        AccountantInvoicingProjectCandidateEntryView RetrieveAccountantProjectCandidateInvoicingEntry(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        AccountantInvoicingProjectCandidateCategoryType1EntryView RetrieveAccountantProjectCandidateCategoryType1InvoicingEntry(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        AccountantInvoicingProjectCandidateCategoryType2EntryView RetrieveAccountantProjectCandidateCategoryType2InvoicingEntry(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        AccountantInvoicingProjectCandidateCategoryType3EntryView RetrieveAccountantProjectCandidateCategoryType3InvoicingEntry(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        AccountantInvoicingProjectProductEntryView RetrieveAccountantProjectProductInvoicingEntry(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        AccountantInvoicingAcdcProjectFixedPriceEntryView RetrieveAccountantAcdcProjectFixedPriceInvoicingEntry(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        AccountantInvoicingConsultancyProjectFixedPriceEntryView RetrieveAccountantConsultancyProjectFixedPriceInvoicingEntry(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        AccountantInvoicingProductSheetEntryView RetrieveAccountantProductSheetEntryInvoicingEntry(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        AccountantInvoicingTimesheetEntryView RetrieveAccountantTimesheetEntryInvoicingEntry(Guid id);
        #endregion
    }
}