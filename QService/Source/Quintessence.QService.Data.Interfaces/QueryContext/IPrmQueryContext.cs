using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QService.Data.Interfaces.QueryContext
{
    /// <summary>
    /// Interface for the Project Management data context
    /// </summary>
    public interface IPrmQueryContext : IQuintessenceQueryContext
    {
        DbQuery<ProjectTypeView> ProjectTypes { get; }
        DbQuery<ProjectView> Projects { get; }
        DbQuery<AssessmentDevelopmentProjectView> AssessmentDevelopmentProjects { get; }
        DbQuery<ConsultancyProjectView> ConsultancyProjects { get; }
        DbQuery<ProjectTypeCategoryView> ProjectTypeCategories { get; }
        DbQuery<ProjectTypeCategoryDefaultValueView> ProjectTypeCategorieDefaultValues { get; }
        DbQuery<ProjectCategoryDetailView> ProjectCategoryDetails { get; }
        DbQuery<ProjectCategoryDetailDictionaryIndicatorView> ProjectCategoryDetailDictionaryIndicators { get; }
        DbQuery<ProjectCategoryDetailSimulationCombinationView> ProjectCategoryDetailDictionaryCombinations { get; }
        DbQuery<ProjectRoleView> ProjectRoles { get; }
        DbQuery<ProjectRoleDictionaryLevelView> ProjectRoleDictionaryLevels { get; }
        DbQuery<ProjectCategoryDetailCompetenceSimulationView> ProjectCategoryDetailCompetenceSimulations { get; }
        DbQuery<ProjectCandidateView> ProjectCandidates { get; }
        DbQuery<ProjectPlanView> ProjectPlans { get; }
        DbQuery<ProjectPlanPhaseView> ProjectPlanPhases { get; }
        DbQuery<ProjectPlanPhaseEntryView> ProjectPlanPhaseEntries { get; }
        DbQuery<ProjectPriceIndexView> ProjectPriceIndices { get; }
        DbQuery<TimesheetEntryView> TimesheetEntries { get; }
        DbQuery<ProjectFixedPriceView> ProjectFixedPrices { get; }
        DbQuery<ProductsheetEntryView> ProductsheetEntries { get; }
        DbQuery<SubProjectView> SubProjects { get; }
        DbQuery<MainProjectView> MainProjects { get; }
        DbQuery<ProjectCandidateIndicatorSimulationScoreView> ProjectCandidateIndicatorSimulationScores { get; }
        DbQuery<ProjectCandidateCompetenceSimulationScoreView> ProjectCandidateCompetenceSimulationScores { get; }
        DbQuery<ProjectCandidateIndicatorSimulationFocusedScoreView> ProjectCandidateIndicatorSimulationFocusedScores { get; }
        DbQuery<ProjectCandidateClusterScoreView> ProjectCandidateClusterScores { get; }
        DbQuery<ProjectCandidateCompetenceScoreView> ProjectCandidateCompetenceScores { get; }
        DbQuery<ProjectCandidateIndicatorScoreView> ProjectCandidateIndicatorScores { get; }
        DbQuery<ProjectCandidateResumeView> ProjectCandidateResumes { get; }
        DbQuery<ProjectCandidateResumeFieldView> ProjectCandidateResumeFields { get; }
        DbQuery<AdviceView> Advices { get; }
        DbQuery<ProjectCandidateDetailView> ProjectCandidateDetails { get; }
        DbQuery<ProposalView> Proposals { get; }
        DbQuery<ProjectTypeCategoryUnitPriceView> ProjectTypeCategoryUnitPrices { get; }
        DbQuery<ProjectTypeCategoryLevelView> ProjectTypeCategoryLevels { get; }
        DbQuery<FrameworkAgreementView> FrameworkAgreements { get; }
        DbQuery<ProjectCandidateReportRecipientView> ProjectCandidateReportRecipients { get; }
        DbQuery<ProjectDocumentMetadataView> ProjectDocumentMetadatas { get; }
        DbQuery<ProjectCandidateCategoryDetailTypeView> ProjectCandidateCategoryDetailTypes { get; }
        DbQuery<ProjectCandidateProjectView> ProjectCandidateProjects { get; }
        DbQuery<ReportStatusView> ReportStatuses { get; }
        DbQuery<ProjectDnaView> ProjectDnas { get; }
        DbQuery<ProjectProductView> ProjectProducts { get; }
        DbQuery<ProjectEvaluationView> ProjectEvaluations { get; }
        DbQuery<EvaluationFormView> EvaluationForms { get; }
        DbQuery<EvaluationFormAcdcView> EvaluationFormsAcdc { get; }
        DbQuery<EvaluationFormCoachingView> EvaluationFormsCoaching { get; }
        DbQuery<EvaluationFormCustomProjectsView> EvaluationFormsCustomProjects { get; }
        DbQuery<MailStatusTypeView> MailStatusTypes { get; }
        DbQuery<EvaluationFormTypeView> EvaluationFormTypes { get; }
        DbQuery<ProjectComplaintView> ProjectComplaints { get; }
        DbQuery<ComplaintTypeView> ComplaintTypes { get; }
        DbQuery<ProjectReportRecipientView> ProjectReportRecipients { get; }
        DbQuery<TheoremListRequestView> TheoremListRequests { get; }
        DbQuery<TheoremListView> TheoremLists { get; }
        DbQuery<TheoremListTemplateView> TheoremListTemplates { get; }

        IEnumerable<RecentProjectView> ListRecentProjects(Guid userId, int maxProjects);
        IEnumerable<SearchCrmProjectResultItemView> SearchCrmProjects(Guid? projectId, string projectName, bool withPlannedStatus, bool withRunningStatus, bool withDoneStatus, bool withStoppedStatus);
        IEnumerable<int> ListProposalYears();
        IEnumerable<int> ListFrameworkAgreementYears();
        IEnumerable<ProjectDnaSelectedTypeView> ListProjectDnaTypes(Guid projectDnaId);
        IEnumerable<ProjectDnaSelectedContactPersonView> ListProjectDnaContactPersons(Guid projectDnaId);
        IEnumerable<NeopirScoreView> ListNeopirScores(Guid projectCandidateId);
        IEnumerable<LeaderScoreView> ListLeiderschapScores(Guid projectCandidateId);
        IEnumerable<ProjectCandidateRoiScoreView> ListRoiScores(Guid projectCandidateId);
        IEnumerable<ProjectCandidateReportRecipientView> ListProjectCandidateReportRecipients(Guid projectCandidateId);
        IEnumerable<ProjectCategoryDetailDictionaryIndicatorView> ListProjectCategoryDetailDictionaryIndicators(Guid projectCategoryDetailId, int? languageId = null);
        IEnumerable<ProjectCandidateOverviewEntryView> RetrieveProjectCandidateAssistantOverview(DateTime? startDate, DateTime? endDate, Guid? projectCandidateId = null, Guid? customerAssistantId = null);
        IEnumerable<ProjectCandidateOverviewEntryView> RetrieveProjectCandidateReservedAssistantOverview(DateTime? startDate, DateTime? endDate = null, Guid? customerAssistantId = null);
        IEnumerable<ProjectCandidateOverviewEntryView> RetrieveProjectCandidateCategoryType1AssistantOverview(DateTime? startDate, DateTime? endDate = null, Guid? projectCandidateCategoryDetailTypeId = null, Guid? customerAssistantId = null);
        IEnumerable<ProjectCandidateOverviewEntryView> RetrieveProjectCandidateCategoryType2AssistantOverview(DateTime? startDate, DateTime? endDate = null, Guid? projectCandidateCategoryDetailTypeId = null, Guid? customerAssistantId = null);
        IEnumerable<ProjectCandidateOverviewEntryView> RetrieveProjectCandidateCategoryType3AssistantOverview(DateTime? startDate, DateTime? endDate = null, Guid? projectCandidateCategoryDetailTypeId = null, Guid? customerAssistantId = null);
        IEnumerable<ProjectCandidateOverviewEntryView> RetrieveProjectCandidateCancelledAssistantOverview(DateTime? startDate, DateTime? endDate = null, Guid? customerAssistantId = null);
        IEnumerable<ProjectCandidateReportingOverviewEntryView> RetrieveProjectCandidateReportingAssistantOverview(DateTime? startDate, Guid? projectCandidateId, Guid? customerAssistantId);
        IEnumerable<ProjectInvoiceAmountOverviewEntryView> ListProjectInvoiceAmountOverviewEntries(Guid id);
        IEnumerable<SimulationContextLoginView> ListSimulationContextLogins(Guid projectCandidateId);
        IEnumerable<ProjectRoleDictionaryLevelView> ListProjectRoleDictionaryLevels(Guid projectRoleId, int? languageId = null);
    }
}
