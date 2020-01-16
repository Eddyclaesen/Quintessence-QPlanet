using System;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.DataModel.Prm;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QueryModel.Fin;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts
{
    [ServiceContract]
    public interface IProjectManagementQueryService
    {
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectTypeView> ListProjectTypes();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectView RetrieveProject(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectView RetrieveProjectDetail(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ListProjectsResponse ListRecentProjects(ListProjectsRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectView RetrieveProjectWithCrmProjects(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<SearchCrmProjectResultItemView> SearchCrmProjects(SearchCrmProjectsRequest id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectStatusCodeViewType> ListPossibleStatusses(int statusCode);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        AssessmentDevelopmentProjectView RetrieveAssessmentDevelopmentProjectDetail(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ConsultancyProjectView RetrieveConsultancyProjectDetail(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectTypeCategoryView> ListAvailableProjectCategories(Guid projectTypeId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectCategoryDetailView RetrieveMainProjectCategoryDetail(Guid projectId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectCategoryDetailView RetrieveMainProjectCategoryDetailDetail(Guid projectId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ListProjectsResponse ListProjects(ListProjectsRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectCategoryDetailDictionaryIndicatorView> ListProjectCategoryDetailDictionaryIndicators(ListProjectCategoryDetailDictionaryIndicatorsRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectCategoryDetailSimulationCombinationView> ListProjectCategoryDetailSimulationCombinations(Guid projectCategoryDetailId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectRoleDictionaryLevelView> ListProjectRoleDictionaryIndicators(ListProjectRoleDictionaryIndicatorsRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectRoleView> ListProjectRolesForQuintessence();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectRoleView> ListProjectRolesForContacts();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectRoleView RetrieveProjectRole(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectCategoryDetailCompetenceSimulationView> ListProjectCategoryDetailCompetenceSimulations(Guid projectCategoryDetailId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ListProjectCandidatesResponse ListProjectCandidateDetailsByProjectId(Guid projectId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        void ValidateProject(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectPlanView RetrieveProjectPlanDetail(RetrieveProjectPlanDetailRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectPlanPhaseView RetrieveProjectPlanPhase(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectPlanPhaseView RetrieveProjectPlanPhaseDetail(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectPlanPhaseActivityView RetrieveProjectPlanPhaseActivity(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectPriceIndexView> ListProjectPriceIndices(Guid projectId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectPriceIndexView RetrieveProjectPriceIndex(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<TimesheetEntryView> ListAllProjectTimesheets(Guid projectId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProductsheetEntryView> ListProjectUserProductsheets(Guid projectId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<TimesheetEntryView> RetrieveProjectTimesheet(Guid projectId, Guid? userId, int year, int month, bool isProjectManager = false);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProductsheetEntryView> RetrieveProjectProductsheet(Guid projectId, int? year, int? month);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<TimesheetEntryView> ListProjectTimesheets(Guid projectId, DateTime dateFrom, DateTime dateTo);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<TimesheetEntryView> ListUserTimesheets(int year, int month, Guid userId, bool isProjectManager);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectPlanPhaseProductView RetrieveProjectPlanPhaseProduct(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectPlanPhaseEntryView RetrieveProjectPlanPhaseEntry(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectPlanPhaseActivityView> ListRelatedProjectPlanPhaseActivities(Guid projectPlanPhaseId, Guid activityId, Guid profileId, decimal duration);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectPlanPhaseProductView> ListRelatedProjectPlanPhaseProducts(Guid projectPlanPhaseId, Guid productId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectPlanPhaseProductView> ListProjectPlanPhaseProducts(Guid projectPlanPhaseId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectFixedPriceView> ListProjectFixedPrices(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectFixedPriceView RetrieveProjectFixedPrice(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectPlanPhaseView> ListActiveProjectPlanPhases(Guid projectId, int? year, int? month);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<SubProjectView> ListSubProjects(Guid projectId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectRoleView> ListProjectRolesForQuintessenceAndContact(int contactId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectTypeCategoryView RetrieveProjectTypeCategory(RetrieveProjectTypeCategoryRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectTypeCategoryView> ListSubcategories();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        MainProjectView RetrieveMainProject(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectCandidateView> ListUserProjectCandidates(ListUserProjectCandidatesRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ListProjectCandidateSimulationScoresResponse ListProjectCandidateSimulationScores(Guid projectCandidateId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectCategoryDetailView RetrieveProjectMainCategoryDetail(Guid projectId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectCandidateView RetrieveProjectCandidate(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ListProjectCandidateScoresResponse ListProjectCandidateScores(Guid projectCandidateId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        RetrieveProjectCandidateResumeResponse RetrieveProjectCandidateResume(Guid projectCandidateId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProposalView> ListProposals(int year);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<int> ListProposalYears();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProposalView RetrieveProposal(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectCandidateResumeView> ListProjectCandidateResumes(Guid projectCandidateId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        FrameworkAgreementView RetrieveFrameworkAgreement(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<FrameworkAgreementView> ListFrameworkAgreementsByYear(int year);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<int> ListFrameworkAgreementYears();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectCandidateReportRecipientView> ListProjectCandidateReportRecipientsByProjectCandidateId(Guid projectCandidateId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ReportStatusView> ListReportStatuses();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ReportStatusView RetrieveReportStatusByCode(string code);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectTypeCategoryLevelView> ListProjectTypeCategoryLevels();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectTypeCategoryUnitPriceView RetrieveProjectTypeCategoryUnitPrice(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectCandidateView> ListProjectCandidateDetails(ListProjectCandidateDetailsRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<UnplannedProjectCandidateEvent> ListAvailableEvents(Guid roomId, DateTime dateTime);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectCandidateView RetrieveProjectCandidateDetail(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectCandidateView RetrieveProjectCandidateDetailWithTypes(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectCandidateCategoryDetailTypeView RetrieveProjectCandidateCategoryDetailType(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectCategoryDetailSimulationCombinationView> ListProjectCandidateSimulationCombinations(Guid projectCandidateId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectCandidateCategoryDetailTypeView> ListProjectCandidateCategoryDetailTypes(Guid projectCandidateId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectDnaView RetrieveProjectDna(int crmProjectId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<DayPlanAssessorView> ListProjectCandidateAssessorsForPlanning(int officeId, DateTime dateTime);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectTypeCategoryUnitPriceOverviewResponse ListProjectTypeCategoryUnitPriceOverview();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectTypeCategoryUnitPriceView> ListProjectTypeCategoryUnitPricesByCategory(Guid projectTypeCategoryId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        InvoiceOverviewResponse ListInvoiceOverview(Guid projectId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectProductView> ListProjectProducts(Guid projectId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectProductView RetrieveProjectProduct(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectEvaluationView RetrieveProjectEvaluationByCrmProject(int crmProjectId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectEvaluationView RetrieveProjectEvaluation(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        string CreateEvaluationFormVerificationCode();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        EvaluationFormView RetrieveEvaluationForm(RetrieveEvaluationFormRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<EvaluationFormView> ListEvaluationFormsByCrmProject(int crmProjectId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectComplaintView> ListProjectComplaintsByCrmProject(int crmProjectId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<EvaluationFormTypeView> ListEvaluationFormTypes();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<MailStatusTypeView> ListMailStatusTypes();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectTypeCategoryUnitPriceView RetrieveProjectTypeCategoryUnitPriceByTypeAndLevel(Guid projectTypeCategoryId, Guid projectTypeCategoryLevelId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ComplaintTypeView> ListComplaintTypes();

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectComplaintView RetrieveProjectComplaint(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ListProductScoresResponse ListProductScores(ListProductScoresRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        RetrieveProjectCandidateReportingResponse RetrieveProjectCandidateReporting(Guid projectCandidateId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ConsultancyProjectView RetrieveProjectByProjectPlan(Guid projectPlanId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectCategoryDetailView> ListProjectCategoryDetails(Guid projectId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectReportRecipientView> ListProjectReportRecipientsByProjectId(Guid projectId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ListProjectCandidateOverviewResponse ListProjectCandidateOverviewEntries(ListProjectCandidateOverviewRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectCandidateOverviewEntryView RetrieveProjectCandidateOverviewEntry(Guid id, ProjectCandidateOverviewEntryType overviewEntryType);
        
        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        bool ValidateQCareVerificationCode(string verificationCode);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<TheoremListRequestView> ListCulturalFitContactRequests(int contactId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        TheoremListRequestView RetrieveTheoremListRequest(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectCategoryDetailView RetrieveProjectCategoryDetail(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectCandidateReportingOverviewEntryView> ListProjectCandidateReportingOverviewEntries(ListProjectCandidateReportingOverviewEntriesRequest request);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectCandidateReportingOverviewEntryView RetrieveProjectCandidateReportingOverviewEntry(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        bool HasCandidates(Guid projectId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        bool HasProjectProducts(Guid projectId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        bool HasProjectFixedPrices(Guid projectId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        TheoremListRequestView RetrieveTheoremListRequestByProjectAndCandidate(Guid projectId, Guid candidateId);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectInvoiceAmountOverviewEntryView> ListProjectInvoiceAmountOverviewEntries(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<SimulationContextLoginView> ListSimulationContextLogins(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        List<ProjectCandidateView> ListProjectCandidates(Guid id);

        [OperationContract]
        [FaultContract(typeof(ValidationContainer))]
        ProjectCandidateView RetrieveProjectCandidateDetailExtended(Guid id);
    }
}
