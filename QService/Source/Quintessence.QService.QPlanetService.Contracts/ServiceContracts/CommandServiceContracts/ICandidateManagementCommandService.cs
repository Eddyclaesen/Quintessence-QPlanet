using System;
using System.ServiceModel;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CandidateManagement;

namespace Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts
{
    [ServiceContract]
    public interface ICandidateManagementCommandService
    {
        #region Create methods
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Guid CreateCandidate(CreateCandidateRequest request);
        #endregion

        #region Update methods
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateCandidate(UpdateCandidateRequest request);
        #endregion

        #region Delete methods
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteCandidate(Guid id);
        #endregion

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void ChangeCandidateLanguage(int languageId, Guid candidateId);
        
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void SetCandidateQCandidateUserId(Guid candidateId, Guid qCandidateUserId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void CreateProgramComponent(CreateProgramComponentRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProgramComponentEnd(UpdateProgramComponentEndRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProgramComponentStart(UpdateProgramComponentStartRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void UpdateProgramComponent(UpdateProgramComponentRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteProgramComponent(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteProjectCandidateProgramComponents(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void DeleteRoomProgramComponents(Guid id);
    }
}
