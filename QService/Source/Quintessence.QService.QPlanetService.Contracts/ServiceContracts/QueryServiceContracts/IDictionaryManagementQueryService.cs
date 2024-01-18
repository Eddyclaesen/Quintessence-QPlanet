using System;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.QueryModel.Dim;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement;

namespace Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts
{
    [ServiceContract]
    public interface IDictionaryManagementQueryService
    {
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<DictionaryView> ListQuintessenceDictionaries();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<DictionaryView> ListDetailedDictionaries(ListDetailedDictionariesRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<DictionaryView> ListCustomerDictionaries();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        DictionaryView RetrieveDictionaryDetail(RetrieveDictionaryDetailRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        DictionaryView RetrieveDictionary(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<AvailableDictionaryView> ListAvailableDictionaries(int contactId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<AvailableBceView> ListAvailableBces(int contactId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ListDictionariesResponse ListDictionaries(ListDictionariesRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<DictionaryIndicatorMatrixEntryView> ListDictionaryIndicatorMatrixEntries(ListDictionaryIndicatorMatrixEntriesRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<DictionaryIndicatorView> ListIndicatorsByDictionaryLevel(Guid dictionaryLevelId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<DictionaryIndicatorView> ListIndicatorsByDictionaryCompetence(Guid dictionaryCompetenceId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        DictionaryLevelView RetrieveDictionaryLevel(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<DictionaryIndicatorView> ListDictionaryIndicators(List<Guid> ids);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<DictionaryImportFileInfoView> ListImportDictionaries();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        DictionaryImportView AnalyseDictionary(string name);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        DictionaryAdminView RetrieveDictionaryAdmin(Guid dictionaryId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        DictionaryClusterAdminView RetrieveDictionaryClusterAdmin(Guid dictionaryClusterId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        DictionaryCompetenceAdminView RetrieveDictionaryCompetenceAdmin(Guid dictionaryCompetenceId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        DictionaryLevelAdminView RetrieveDictionaryLevelAdmin(Guid dictionaryLevelId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        DictionaryIndicatorAdminView RetrieveDictionaryIndicatorAdmin(Guid dictionaryIndicatorId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<DictionaryAdminView> ListQuintessenceAdminDictionaries();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<DictionaryAdminView> ListCustomerAdminDictionaries();
    }
}
