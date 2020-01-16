using System;
using System.Web.Mvc;

namespace Quintessence.QPlanet.Webshell.Areas.Project
{
    public class ProjectAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Project";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Project_SearchContact",
                "Project/ProjectGeneral/SearchContact/{term}",
                new { controller = "ProjectGeneral", action = "SearchContact" }
            );

            context.MapRoute(
                "Project_SearchProjectManager",
                "Project/ProjectGeneral/SearchProjectManager/{term}",
                new { controller = "ProjectGeneral", action = "SearchProjectManager" }
            );

            context.MapRoute(
                "Project_ProjectCandidateOverview_ProjectCandidateOverview",
                "Project/ProjectCandidateOverview/ProjectCandidateOverview/{customerAssistantId}",
                new { controller = "ProjectCandidateOverview", action = "ProjectCandidateOverview", customerAssisantId = UrlParameter.Optional }
            );

            context.MapRoute(
                "Project_ProjectGeneral_UnlinkCrmProject",
                "Project/ProjectGeneral/UnlinkCrmProject/{id}/{crmId}",
                new { controller = "ProjectGeneral", action = "UnlinkCrmProject" }
            );

            context.MapRoute(
                "Project_ProjectGeneral_ProjectDna",
                "Project/ProjectGeneral/ProjectDna/{crmProjectId}",
                new { controller = "ProjectGeneral", action = "ProjectDna" }
            );

            context.MapRoute(
                "Project_ProjectGeneral_ProjectDna_EvaluationOverview",
                "Project/ProjectGeneral/EvaluationOverview/{crmProjectId}",
                new { controller = "ProjectGeneral", action = "EvaluationOverview" }
            );

            context.MapRoute(
                "Project_ProjectGeneral_ProjectDna_ProjectComplaint",
                "Project/ProjectGeneral/ProjectComplaint/{crmProjectId}",
                new { controller = "ProjectGeneral", action = "ProjectComplaint" }
            );

            context.MapRoute(
                "Project_ProjectGeneral_Create",
                "Project/ProjectGeneral/Create/{id}/{projectCandidateId}",
                new { controller = "ProjectGeneral", action = "Create", id = UrlParameter.Optional, projectCandidateId = UrlParameter.Optional }
            );

            context.MapRoute(
                "Project_ProjectGeneral_GenerateCandidateReport",
                "Project/ProjectGeneral/GenerateCandidateReport/{candidateReportDefinitionId}/{projectCandidateId}",
                new { controller = "ProjectGeneral", action = "GenerateCandidateReport" }
            );

            context.MapRoute(
                "Project_ProjectProposal_ProposalOverview",
                "Project/ProjectProposal/ProposalOverview/{year}",
                new { controller = "ProjectProposal", action = "ProposalOverview" }
            );

            context.MapRoute(
                "Project_ProjectFrameworkAgreement_FrameworkAgreements",
                "Project/ProjectFrameworkAgreement/FrameworkAgreements/{year}",
                new { controller = "ProjectFrameworkAgreement", action = "FrameworkAgreements" }
            );

            context.MapRoute(
                "Project_ProjectGeneral_default",
                "Project/ProjectGeneral/{action}/{id}",
                new { controller = "ProjectGeneral", action = "ProjectDetail" }
            );

            context.MapRoute(
                "Project_ProjectAssessmentDevelopment_MainProjectCategoryDetail",
                "Project/ProjectAssessmentDevelopment/MainProjectCategoryDetail/{id}",
                new { controller = "ProjectAssessmentDevelopment", action = "MainProjectCategoryDetail" });

            context.MapRoute(
                "Project_ProjectAssessmentDevelopment_ChangeCandidateLanguage",
                "Project/ProjectAssessmentDevelopment/ChangeCandidateLanguage/{LanguageId}/{CandidateId}",
                new { controller = "ProjectAssessmentDevelopment", action = "ChangeCandidateLanguage" });

            context.MapRoute(
                "Project_ProjectAssessmentDevelopment_SendCandidateInvitationMail",
                "Project/ProjectAssessmentDevelopment/SendCandidateInvitationMail/{id}/{LanguageId}",
                new { controller = "ProjectAssessmentDevelopment", action = "SendCandidateInvitationMail" });

            context.MapRoute(
                "Project_ProjectAssessmentDevelopment_SendCulturalFitInvitationMail",
                "Project/ProjectAssessmentDevelopment/SendCulturalFitInvitationMail/{id}/{LanguageId}",
                new { controller = "ProjectAssessmentDevelopment", action = "SendCulturalFitInvitationMail" });

            context.MapRoute(
                "Project_ProjectAssessmentDevelopment_CreateCulturalFitReport",
                "Project/ProjectAssessmentDevelopment/CreateCulturalFitReport/{id}/{LanguageId}",
                new { controller = "ProjectAssessmentDevelopment", action = "CreateCulturalFitReport" });

            context.MapRoute(
                "Project_ProjectAssessmentDevelopment_ListCulturalFitContactRequests",
                "Project/ProjectAssessmentDevelopment/ListCulturalFitContactRequests/{contactId}",
                new { controller = "ProjectAssessmentDevelopment", action = "ListCulturalFitContactRequests" });

            context.MapRoute(
                "Project_ProjectAssessmentDevelopment_CreateCulturalFitContactRequest",
                "Project/ProjectAssessmentDevelopment/CreateCulturalFitContactRequest/{contactId}/{projectId}",
                new { controller = "ProjectAssessmentDevelopment", action = "CreateCulturalFitContactRequest" });

            context.MapRoute(
               "Project_ProjectAssessmentDevelopment_ListProjectCandidateReportRecipients",
               "Project/ProjectAssessmentDevelopment/ListProjectCandidateReportRecipients/{ProjectCandidateId}",
               new { controller = "ProjectAssessmentDevelopment", action = "ListProjectCandidateReportRecipients" });

            context.MapRoute(
               "Project_ProjectAssessmentDevelopment_ListCrmEmails",
               "Project/ProjectAssessmentDevelopment/ListCrmEmails/{ContactId}",
               new { controller = "ProjectAssessmentDevelopment", action = "ListCrmEmails" });

            context.MapRoute(
               "Project_ProjectAssessmentDevelopment_AddProjectCandidateReportRecipients",
               "Project/ProjectAssessmentDevelopment/AddProjectCandidateReportRecipients/{ProjectCandidateId}",
               new { controller = "ProjectAssessmentDevelopment", action = "AddProjectCandidateReportRecipients" });

            context.MapRoute(
                "Project_ProjectAssessmentDevelopment_SubProjectCategoryDetails",
                "Project/ProjectAssessmentDevelopment/SubProjectCategoryDetails/{id}",
                new { controller = "ProjectAssessmentDevelopment", action = "SubProjectCategoryDetails" });

            context.MapRoute(
                "Project_ProjectAssessmentDevelopment_DictionaryIndicators",
                "Project/ProjectAssessmentDevelopment/DictionaryIndicators/{projectCategoryDetailId}/{dictionaryId}",
                new { controller = "ProjectAssessmentDevelopment", action = "DictionaryIndicators" });

            context.MapRoute(
                "Project_ProjectAssessmentDevelopment_DictionaryLevels",
                "Project/ProjectAssessmentDevelopment/DictionaryLevels/{projectCategoryDetailId}/{dictionaryId}",
                new { controller = "ProjectAssessmentDevelopment", action = "DictionaryLevels" });

            context.MapRoute(
                "Project_ProjectAssessmentDevelopment_AddSimulationCompetence",
                "Project/ProjectAssessmentDevelopment/AddSimulationCompetence/{projectCategoryDetailId}/{competenceId}/{combinationId}",
                new { controller = "ProjectAssessmentDevelopment", action = "AddSimulationCompetence" });

            context.MapRoute(
                "Project_ProjectAssessmentDevelopment_RemoveSimulationCompetence",
                "Project/ProjectAssessmentDevelopment/RemoveSimulationCompetence/{projectCategoryDetailId}/{competenceId}/{combinationId}",
                new { controller = "ProjectAssessmentDevelopment", action = "RemoveSimulationCompetence" });

            context.MapRoute(
                "Project_ProjectAssessmentDevelopment_RemoveDictionaryCompetence",
                "Project/ProjectAssessmentDevelopment/RemoveDictionaryCompetence/{projectCategoryDetailId}/{competenceId}",
                new { controller = "ProjectAssessmentDevelopment", action = "RemoveDictionaryCompetence" });

            context.MapRoute(
                "Project_ProjectAssessmentDevelopment_UpdateSimulationInformation",
                "Project/ProjectAssessmentDevelopment/UpdateSimulationInformation",
                new { controller = "ProjectAssessmentDevelopment", action = "UpdateSimulationInformation" });

            context.MapRoute(
                "Project_ProjectAssessmentDevelopment_ChangeProjectRole",
                "Project/ProjectAssessmentDevelopment/ChangeProjectRole",
                new { controller = "ProjectAssessmentDevelopment", action = "ChangeProjectRole" });

            context.MapRoute(
                "Project_ProjectConsultancy_CreateProjectPlanPhaseActivity",
                "Project/ProjectConsultancy/CreateProjectPlanPhaseActivity/{id}/{projectId}",
                new { controller = "ProjectConsultancy", action = "CreateProjectPlanPhaseActivity" });

            context.MapRoute(
                "Project_ProjectConsultancy_CreateProjectPlanPhaseProduct",
                "Project/ProjectConsultancy/CreateProjectPlanPhaseProduct/{id}/{projectId}",
                new { controller = "ProjectConsultancy", action = "CreateProjectPlanPhaseProduct" });

            context.MapRoute(
                "Project_ProjectConsultancy_EditTimesheet",
                "Project/ProjectConsultancy/EditTimesheet/{id}/{year}/{month}/{userId}",
                new { controller = "ProjectConsultancy", action = "EditTimesheet", year = UrlParameter.Optional, month = UrlParameter.Optional, userId = UrlParameter.Optional });

            context.MapRoute(
                "Project_ProjectConsultancy_EditProductsheet",
                "Project/ProjectConsultancy/EditProductsheet/{id}/{year}/{month}",
                new { controller = "ProjectConsultancy", action = "EditProductsheet", year = UrlParameter.Optional, month = UrlParameter.Optional });

            context.MapRoute(
                "Project_ProjectConsultancy_ProjectTimesheets",
                "Project/ProjectConsultancy/ProjectTimesheets/{id}/{year}/{month}",
                new { controller = "ProjectConsultancy", action = "ProjectTimesheets" });

            context.MapRoute(
                "Project_ProjectProposalDetail_EditProposalStatus",
                "Project/ProjectProposalDetail/EditProposalStatus/{id}/{statusCode}",
                new { controller = "ProjectProposalDetail", action = "EditProposalStatus" });

            context.MapRoute(
                "Project_ProjectGeneral_GenerateReport",
                "Project/ProjectGeneral/GenerateReport/{id}/{code}/{reportDefinitionId}/{outputFormat}",
                new { controller = "ProjectGeneral", action = "GenerateReport" });

            context.MapRoute(
                "Project_ProjectAssessmentDevelopment_UnmarkDocumentAsImportant",
                "Project/ProjectAssessmentDevelopment/UnmarkDocumentAsImportant/{projectId}/{uniqueId}",
                new { controller = "ProjectAssessmentDevelopment", action = "UnmarkDocumentAsImportant" });

            context.MapRoute(
                "Project_ProjectAssessmentDevelopment_MarkDocumentAsImportant",
                "Project/ProjectAssessmentDevelopment/MarkDocumentAsImportant/{projectId}/{uniqueId}",
                new { controller = "ProjectAssessmentDevelopment", action = "MarkDocumentAsImportant" });

            context.MapRoute(
                "Project_ProjectAssessmentDevelopment_MarkProjectCandidateDetailDictionaryIndicatorAsStandard",
                "Project/ProjectAssessmentDevelopment/MarkProjectCandidateDetailDictionaryIndicatorAsStandard/{id}/{isChecked}",
                new { controller = "ProjectAssessmentDevelopment", action = "MarkProjectCandidateDetailDictionaryIndicatorAsStandard" });

            context.MapRoute(
                "Project_ProjectAssessmentDevelopment_MarkProjectCandidateDetailDictionaryIndicatorAsDistinctive",
                "Project/ProjectAssessmentDevelopment/MarkProjectCandidateDetailDictionaryIndicatorAsDistinctive/{id}/{isChecked}",
                new { controller = "ProjectAssessmentDevelopment", action = "MarkProjectCandidateDetailDictionaryIndicatorAsDistinctive" });

            context.MapRoute(
                "Project_ProjectAssessmentDevelopment_GenerateCulturalFitReport",
                "Project/ProjectAssessmentDevelopment/GenerateCulturalFitReport/{id}/{languageId}",
                new { controller = "ProjectAssessmentDevelopment", action = "GenerateCulturalFitReport" });

            context.MapRoute(
                "Project_ProjectConsultancy_UnmarkDocumentAsImportant",
                "Project/ProjectConsultancy/UnmarkDocumentAsImportant/{projectId}/{uniqueId}",
                new { controller = "ProjectConsultancy", action = "UnmarkDocumentAsImportant" });

            context.MapRoute(
                "Project_ProjectConsultancy_MarkDocumentAsImportant",
                "Project/ProjectConsultancy/MarkDocumentAsImportant/{projectId}/{uniqueId}",
                new { controller = "ProjectConsultancy", action = "MarkDocumentAsImportant" });

            context.MapRoute(
                "Project_ProjectConsultancy_AddActivityDetailTrainingCandidate",
                "Project/ProjectConsultancy/AddActivityDetailTrainingCandidate/{activityDetailTrainingId}/{appointmentId}",
                new { controller = "ProjectConsultancy", action = "AddActivityDetailTrainingCandidate" });

            context.MapRoute(
                "Project_default",
                "Project/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
