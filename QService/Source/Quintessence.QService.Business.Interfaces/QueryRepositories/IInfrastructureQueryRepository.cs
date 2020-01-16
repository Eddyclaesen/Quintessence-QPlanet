using System;
using System.Collections;
using System.Collections.Generic;
using Quintessence.QService.QueryModel.Inf;

namespace Quintessence.QService.Business.Interfaces.QueryRepositories
{
    public interface IInfrastructureQueryRepository : IQueryRepository
    {
        List<LanguageView> ListLanguages();
        List<AssessmentRoomView> ListAssessmentRooms();
        OfficeView RetrieveOffice(int id);
        OfficeView RetrieveOffice(string shortName);
        List<AssessmentRoomView> ListOfficeAssessmentRooms(int officeId);
        MailTemplateView RetrieveMailTemplateByCode(string code);
        MailTemplateView RetrieveMailTemplate(Guid id);
        List<MailTemplateTagView> ListMailTemplateTags(string storedProcedureName, Guid id);
        List<MailTemplateTagView> ListMailTemplateTags(string storedProcedureName, Guid id, int languageId);
        List<JobDefinitionView> ListJobDefinitions();
    }
}
