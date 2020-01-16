using System;
using System.Collections.Generic;
using System.ServiceModel;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.DataModel.Cam;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CandidateManagement;
using Quintessence.QService.QueryModel.Cam;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts
{
    [ServiceContract]
    public interface ICandidateManagementQueryService
    {
        #region Retrieve methods
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        CandidateView RetrieveCandidate(Guid id);
        #endregion

        #region List methods
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<CandidateView> ListCandidates();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<CandidateView> ListCandidatesByFullName(ListCandidatesByFullNameRequest listCandidatesByFullNameRequest);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        RetrieveDayProgramDashboardResponse RetrieveDayProgramDashboard(RetrieveDayProgramDashboardRequest request);
        #endregion

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProgramComponentView> RetrieveAssessmentRoomProgram(Guid assessmentRoomId, DateTime dateTime);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProgramComponentView> ListProgramComponentsByCandidate(Guid projectCandidateId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProgramComponentView RetrieveProgramComponent(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<Guid> CheckForProgramComponentCollisions(int officeid, DateTime dateTime);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProgramComponentSpecialView> ListSpecialEvents();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        Dictionary<string, List<string>> CheckForUnplannedEvents(int officeId, DateTime dateTime);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProgramComponentView> ListProgramComponentsByUser(Guid userId, DateTime start, DateTime end);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProgramComponentView> ListProgramComponentsByDate(DateTime dateTime);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProgramComponentView> ListProgramComponentsByRoom(Guid roomId, DateTime dateTime);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProgramComponentView RetrieveLinkedProgramComponent(Guid programComponentId);
    }
}
