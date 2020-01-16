using System;
using System.Collections.Generic;
using Quintessence.QService.Core.Performance;
using Quintessence.QService.QueryModel.Cam;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QService.Business.Interfaces.QueryRepositories
{
    public interface ICandidateManagementQueryRepository : IQueryRepository
    {
        List<CandidateView> ListCandidates();
        List<CandidateView> ListCandidatesByFullName(string firstName, string lastName);
        CandidateView RetrieveCandidateDetail(Guid id);
        List<ProgramComponentView> ListProgramComponents(DateTime date);
        List<ProgramComponentView> ListProgramComponentsByAssessmentRoom(Guid assessmentRoomId, DateTime date);
        List<ProgramComponentView> ListProgramComponentsByCandidate(Guid projectCandidateId);
        List<ProgramComponentView> ListProgramComponentsByOfficeId(int officeId, DateTime dateTime);
        bool CheckForCollisions(ProgramComponentView programComponent);
        List<ProgramComponentView> ListProgramComponentsByUser(Guid userId, DateTime start, DateTime end);
        List<ProgramComponentView> ListProgramComponentsByDate(DateTime start, DateTime end);
        List<ProgramComponentView> ListProgramComponentsByRoom(Guid roomId, DateTime start, DateTime end);
        ProgramComponentView RetrieveLinkedProgramComponent(Guid projectCandidateId, Guid? simulationCombinationId, int simulationCombinationTypeCode);
    }
}
