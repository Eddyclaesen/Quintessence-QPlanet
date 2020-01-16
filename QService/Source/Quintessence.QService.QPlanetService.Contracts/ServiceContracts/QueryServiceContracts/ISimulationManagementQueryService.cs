using System;
using System.Collections.Generic;
using System.ServiceModel;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SimulationManagement;
using Quintessence.QService.QueryModel.Sim;

namespace Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts
{
    [ServiceContract]
    public interface ISimulationManagementQueryService
    {
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ListSimulationSetsResponse ListSimulationSets(ListSimulationSetsRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ListSimulationDepartmentsResponse ListSimulationDepartments(ListSimulationDepartmentsRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ListSimulationLevelsResponse ListSimulationLevels(ListSimulationLevelsRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ListSimulationsResponse ListSimulations(ListSimulationsRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ListSimulationMatrixEntriesResponse ListSimulationMatrixEntries(ListSimulationMatrixEntriesRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        SimulationSetView RetrieveSimulationSet(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        SimulationDepartmentView RetrieveSimulationDepartment(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        SimulationLevelView RetrieveSimulationLevel(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        SimulationView RetrieveSimulation(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        SimulationMatrixEntryView RetrieveSimulationMatrixEntry(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<SimulationCombinationLanguageView> ListSimulationCombinationLangauges(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        SimulationCombinationView RetrieveSimulationCombination(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<SimulationContextView> ListSimulationContexts();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        SimulationContextView RetrieveSimulationContext(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<SimulationContextUserView> ListSimulationContextUsers(Guid simulationContextId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<SimulationTranslationView> ListSimulationTranslations(Guid simulationId);
    }
}
