using System;
using System.Collections.Generic;
using Quintessence.QService.Core.Performance;
using Quintessence.QService.DataModel.Prm;
using Quintessence.QService.QueryModel.Fin;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QService.Business.Interfaces.QueryRepositories
{
    public interface IProjectManagementQueryRepository : IQueryRepository
    {
        List<ProjectTypeView> ListProjectTypes();
        ProjectView RetrieveProject(Guid id);
        ProjectView RetrieveProjectDetail(Guid id);
        List<ProjectView> ListRecentProjects(Guid userId, PagingInfo pagingInfo = null);
        ProjectView RetrieveProjectWithCrmProjects(Guid id);
        List<SearchCrmProjectResultItemView> SearchCrmProjects(Guid? projectId, string projectName = null, bool withPlannedStatus = false, bool withRunningStatus = false, bool withDoneStatus = false, bool withStoppedStatus = false);
        AssessmentDevelopmentProjectView RetrieveAssessmentDevelopmentProjectDetail(Guid id);
        ConsultancyProjectView RetrieveConsultancyProjectDetail(Guid id);
        List<ProjectTypeCategoryView> ListAvailableProjectCategories(Guid projectTypeId);
        ProjectCategoryDetailView RetrieveMainProjectCategoryDetail(Guid projectId);
        ProjectCategoryDetailView RetrieveMainProjectCategoryDetailDetail(Guid projectId);
        List<ProjectView> ListProjects(PagingInfo pagingInfo = null);
        List<ProjectCategoryDetailDictionaryIndicatorView> ListProjectCategoryDetailDictionaryIndicators(Guid projectCategoryDetailId, int? languageId);
        List<ProjectCategoryDetailSimulationCombinationView> ListProjectCategoryDetailSimulationCombinations(Guid projectCategoryDetailId);
        List<ProjectRoleView> ListProjectRolesForQuintessence();
        List<ProjectRoleView> ListProjectRolesForContacts();
        List<ProjectRoleDictionaryLevelView> ListProjectRoleDictionaryLevels(Guid projectRoleId, int? languageId);
        List<ProjectCategoryDetailCompetenceSimulationView> ListProjectCategoryDetailCompetenceSimulations(Guid projectCategoryDetailId);
        ProjectPlanView RetrieveProjectPlanDetail(Guid id);
        ProjectPlanPhaseView RetrieveProjectPlanPhaseDetail(Guid id);
        List<ProjectPlanPhaseActivityView> ListRelatedProjectPlanPhaseActivities(Guid projectPlanPhaseId, Guid activityId, Guid profileId, decimal duration);
        List<ProjectPlanPhaseProductView> ListRelatedProjectPlanPhaseProducts(Guid projectPlanPhaseId, Guid productId);
        ProjectPlanPhaseEntryView RetrieveProjectPlanPhaseEntry(Guid id);
        TEntryType RetrieveProjectPlanPhaseEntry<TEntryType>(Guid id) where TEntryType : ProjectPlanPhaseEntryView;
        List<ProjectPriceIndexView> ListProjectPriceIndices(Guid projectId);
        List<TimesheetEntryView> ListProjectTimesheets(Guid? projectId = null, Guid? userId = null, DateTime? dateFrom = null, DateTime? dateTo = null, bool isProjectManager = false);
        ProjectView RetrieveProjectByProjectPlan(Guid projectPlanId);
        List<ProjectFixedPriceView> ListProjectFixedPrices(Guid id);
        List<ProductsheetEntryView> ListProjectProductsheets(Guid? projectId = null, Guid? userId = null, int? year = null, int? month = null);
        List<ProjectPlanPhaseView> ListActiveProjectPlanPhases(Guid projectPlanId, int? year, int? month);
        List<ProjectPlanPhaseProductView> ListProjectPlanPhaseProducts(Guid projectPlanPhaseId);
        List<SubProjectView> ListSubProjects(Guid projectId);
        List<ProjectRoleView> ListProjectRolesForQuintessenceAndContact(int contactId);
        ProjectTypeCategoryView RetrieveProjectTypeCategory(Guid id);
        List<ProjectCandidateView> ListProjectCandidates(Guid? projectId = null, Guid? candidateId = null, DateTime? date = null);
        List<ProjectTypeCategoryView> ListSubcategories();
        MainProjectView RetrieveMainProject(Guid subProjectId);
        List<ProjectCandidateIndicatorScoreView> ListProjectCandidateIndicatorScores(Guid projectCandidateId);
        List<ProjectCandidateIndicatorSimulationScoreView> ListProjectCandidateIndicatorSimulationScores(Guid projectCandidateId);
        List<ProjectCandidateCompetenceSimulationScoreView> ListProjectCandidateCompetenceSimulationScores(Guid projectCandidateId);
        List<ProjectCandidateIndicatorSimulationFocusedScoreView> ListProjectCandidateIndicatorSimulationFocusedScores(Guid projectCandidateId);
        ProjectCandidateView RetrieveProjectCandidate(Guid id);
        ProjectCandidateView RetrieveProjectCandidateDetail(Guid id);
        ProjectCandidateView RetrieveProjectCandidateDetailWithTypes(Guid id);
        List<ProjectCandidateClusterScoreView> ListProjectCandidateClusterScores(Guid projectCandidateId);
        List<ProjectCandidateCompetenceScoreView> ListProjectCandidateCompetenceScores(Guid projectCandidateId);
        ProjectCandidateResumeView RetrieveProjectCandidateResume(Guid id);
        List<AdviceView> ListAdvices();
        List<ProjectCandidateView> ListUserProjectCandidates(DateTime startDate, DateTime endDate, int? associateId = null);
        List<ProjectCandidateView> ListQCandidateProjectCandidates(DateTime startDate, DateTime endDate, int? associateId = null);
        List<ProjectCandidateView> ListProjectCandidateDetails(Guid projectId);
        List<ProposalView> ListProposalsByYear(int year);
        List<int> ListProposalYears();
        ProposalView RetrieveProposal(Guid id);
        List<ProjectCandidateResumeView> ListProjectCandidateResumes(Guid projectCandidateId);
        FrameworkAgreementView RetrieveFrameworkAgreement(Guid id);
        List<FrameworkAgreementView> ListFrameworkAgreementsByYear(int year);
        List<int> ListFrameworkAgreementYears();
        ProjectCandidateCategoryDetailTypeView RetrieveProjectCandidateCategoryDetailType(Guid id);
        List<ProjectCandidateReportRecipientView> ListProjectCandidateReportRecipientsByProjectCandidateId(Guid projectCandidateId);
        ReportStatusView RetrieveReportStatusByCode(string code);
        List<ProjectDocumentMetadataView> ListProjectDocumentMetadatas(Guid projectId);
        List<ProjectTypeCategoryView> ListProjectTypeCategories();
        List<ProjectTypeCategoryLevelView> ListProjectTypeCategoryLevels();
        List<ProjectTypeCategoryUnitPriceView> ListProjectTypeCategoryUnitPrices();
        List<ProjectTypeCategoryUnitPriceView> ListProjectTypeCategoryUnitPricesByCategory(Guid projectTypeCategoryId);
        List<ProjectCandidateView> ListProjectCandidatesWithCategoryDetailTypes(Guid projectId);
        List<ProjectCandidateCategoryDetailTypeView> ListProjectCandidateCategoryDetailTypes(Guid projectCandidateId);
        List<ProjectCandidateView> ListProjectCandidateDetailsForPlanning(int officeId, DateTime date);
        ProjectDnaView RetrieveProjectDna(int crmProjectId);
        List<ProjectDnaSelectedTypeView> ListDnaTypes(Guid projectDnaId);
        List<ProjectDnaSelectedContactPersonView> ListProjectDnaContactPersons(Guid projectDnaId);
        List<ProjectProductView> ListProjectProducts(Guid projectId);
        ProjectEvaluationView RetrieveProjectEvaluationByCrmProject(int crmProjectId);
        string CreateEvaluationFormVerificationCode();
        List<EvaluationFormView> ListEvaluationFormsByCrmProject(int crmProjectId);
        List<ProjectComplaintView> ListProjectComplaintByCrmProject(int crmProjectId);
        List<EvaluationFormTypeView> ListEvaluationFormTypes();
        List<MailStatusTypeView> ListMailStatusTypes();
        ProjectTypeCategoryUnitPriceView RetrieveProjectTypeCategoryUnitPrice(Guid projectTypeCategoryId, Guid projectTypeCategoryLevelId);
        List<ComplaintTypeView> ListComplaintTypes();
        ProjectComplaintView RetrieveProjectComplaintByCrmProject(int crmProjectId);
        List<NeopirScoreView> ListNeopirScores(Guid projectCandidateId);
        List<LeaderScoreView> ListLeiderschapScores(Guid projectCandidateId);
        List<ProjectCandidateRoiScoreView> ListRoiScores(Guid projectCandidateId);
        List<ProjectCategoryDetailView> ListProjectCategoryDetails(Guid projectId);
        List<ProjectReportRecipientView> ListProjectReportRecipientsByProjectId(Guid projectId);
        List<ProjectCandidateOverviewEntryView> ListProjectCandidateOverviewEntries(DateTime? startDate, DateTime? endDate, Guid? customerAssistantId);
        ProjectCandidateOverviewEntryView RetrieveProjectCandidateOverviewProjectCandidateEntry(Guid id);
        ProjectCandidateOverviewEntryView RetrieveProjectCandidateOverviewProjectCandidateCategoryEntry(Guid id);
        List<ProjectCandidateOverviewEntryView> ListProjectCandidateCategoryOverviewEntries(DateTime? startDate, DateTime? endDate, Guid? customerAssistantId);
        List<ProjectCandidateOverviewEntryView> ListProjectCandidateReservedOverviewEntries(DateTime? startDate, DateTime? endDate, Guid? customerAssistantId);
        List<ProjectCandidateOverviewEntryView> ListProjectCandidateCancelledOverviewEntries(DateTime? startDate, DateTime? endDate, Guid? customerAssistantId);
        EvaluationFormView RetrieveEvaluationFormByCode(string verificationCode);
        bool ValidateQCareVerificationCode(string verificationCode);
        List<TheoremListRequestView> ListCulturalFitContactRequests(int contactId);
        List<ProjectCandidateReportingOverviewEntryView> ListProjectCandidateReportingOverviewEntries(DateTime? startDate, Guid? customerAssistantId);
        ProjectCandidateReportingOverviewEntryView RetrieveProjectCandidateReportingOverviewProjectCandidateEntry(Guid id);
        List<TheoremListRequestView> ListCulturalFitCandidateRequests(Guid candidateId);
        ProjectRoleView RetrieveProjectRole(Guid id);
        bool HasCandidates(Guid projectId);
        bool HasProjectProducts(Guid projectId);
        bool HasProjectFixedPrices(Guid projectId);
        TheoremListRequestView RetrieveTheoremListRequestByProjectAndCandidate(Guid projectId, Guid candidateId);
        List<ProjectInvoiceAmountOverviewEntryView> ListProjectInvoiceAmountOverviewEntries(Guid id);
        List<SimulationContextLoginView> ListSimulationContextLogins(Guid id);
        List<ProjectCandidateView> ListProjectCandidatesWithCandidateDetail(Guid id);
    }
}
