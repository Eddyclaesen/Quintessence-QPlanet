using System;
using System.ServiceModel;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement;

namespace Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts
{
    [ServiceContract]
    public interface IDictionaryManagementCommandService
    {
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void ValidateDictionary(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateDictionary(UpdateDictionaryRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateDictionaryCluster(UpdateDictionaryClusterRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateDictionaryCompetence(UpdateDictionaryCompetenceRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateDictionaryLevel(UpdateDictionaryLevelRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateDictionaryIndicator(UpdateDictionaryIndicatorRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewDictionaryCluster(CreateNewDictionaryClusterRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewDictionaryCompetence(CreateNewDictionaryCompetenceRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewDictionaryLevel(CreateNewDictionaryLevelRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewDictionaryIndicator(CreateNewDictionaryIndicatorRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteDictionaryCluster(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteDictionaryCompetence(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteDictionaryLevel(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteDictionaryIndicator(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid ImportDictionary(ImportDictionaryRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void NormalizeDictionaryClusterOrder(Guid dictionaryId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void NormalizeDictionaryCompetenceOrder(Guid dictionaryClusterId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void NormalizeDictionaryIndicatorOrder(Guid dictionaryLevelId);
    }
}
