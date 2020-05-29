using System;
using System.Collections.Generic;
using System.ServiceModel;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.InfrastructureManagement;
using Quintessence.QService.QueryModel.Inf;

namespace Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts
{
    [ServiceContract]
    public interface IInfrastructureQueryService
    {
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<LanguageView> ListLanguages();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<AssessmentRoomView> ListAssessmentRooms(ListAssessmentRoomsRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        OfficeView RetrieveOffice(RetrieveOfficeRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<OfficeView> ListOffices();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<AssessorColorView> ListAssessorColors();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        AssessmentRoomView RetrieveAssessmentRoom(Guid roomId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        MailTemplateView RetrieveMailTemplateByCode(string code);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        MailTemplateView RetrieveMailTemplate(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        MailTemplateTranslationView RetrieveMailTemplateTranslation(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<MailTemplateView> ListMailTemplates();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<MailTemplateTagView> ListMailTemplateTags(string storedProcedureName, Guid id);

        [OperationContract]
        [FaultContract(typeof (ValidationContainer))]
        CreateEvaluationFormMailResponse CreateEvaluationFormMail(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        CreateProjectCandidateInvitationMailResponse CreateProjectCandidateInvitationMail(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        string RetrieveAllowedPasswordChars();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<JobDefinitionView> ListJobDefinitions();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        CreateCulturalFitInvitationMailResponse CreateCulturalFitInvitationMail (Guid id, int languageId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        SearchResponse Search(string term);
    }
}
