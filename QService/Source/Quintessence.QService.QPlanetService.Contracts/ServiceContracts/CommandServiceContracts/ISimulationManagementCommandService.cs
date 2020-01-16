using System;
using System.Collections.Generic;
using System.ServiceModel;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SimulationManagement;

namespace Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts
{
    [ServiceContract]
    public interface ISimulationManagementCommandService
    {
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteSimulationSet(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteSimulationDepartment(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteSimulationLevel(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteSimulation(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteSimulationCombination(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateSimulationSet(UpdateSimulationSetRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateSimulationDepartment(UpdateSimulationDepartmentRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateSimulationLevel(UpdateSimulationLevelRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateSimulation(UpdateSimulationRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateSimulationCombination(UpdateSimulationCombinationRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateSimulationSet(CreateSimulationSetRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateSimulationDepartment(CreateSimulationDepartmentRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateSimulationLevel(CreateSimulationLevelRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateSimulation(CreateSimulationRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void EnsureSimulationTranslations(Guid simulationId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateSimulationMatrixEntry(CreateSimulationMatrixEntryRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateSimulationContext(UpdateSimulationContextRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateSimulationContextUsers(List<UpdateSimulationContextUserRequest> requests);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewSimulationContextUser(CreateNewSimulationContextUserRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteSimulationContextUser(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void GenerateNewYear(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateNewSimulationContext(CreateNewSimulationContextRequest request);
    }
}
