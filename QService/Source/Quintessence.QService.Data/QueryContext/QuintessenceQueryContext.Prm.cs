using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QService.Data.QueryContext
{
    /// <summary>
    /// Quintessence data context
    /// </summary>
    public partial class QuintessenceQueryContext : IPrmQueryContext
    {
        public DbQuery<ProjectTypeView> ProjectTypes { get { return Set<ProjectTypeView>().AsNoTracking(); } }
        public DbQuery<ProjectView> Projects { get { return Set<ProjectView>().AsNoTracking(); } }
        public DbQuery<AssessmentDevelopmentProjectView> AssessmentDevelopmentProjects { get { return Set<AssessmentDevelopmentProjectView>().AsNoTracking(); } }
        public DbQuery<ConsultancyProjectView> ConsultancyProjects { get { return Set<ConsultancyProjectView>().AsNoTracking(); } }
        public DbQuery<ProjectTypeCategoryView> ProjectTypeCategories { get { return Set<ProjectTypeCategoryView>().AsNoTracking(); } }
        public DbQuery<ProjectTypeCategoryDefaultValueView> ProjectTypeCategorieDefaultValues { get { return Set<ProjectTypeCategoryDefaultValueView>().AsNoTracking(); } }
        public DbQuery<ProjectCategoryDetailView> ProjectCategoryDetails { get { return Set<ProjectCategoryDetailView>().AsNoTracking(); } }
        public DbQuery<ProjectCategoryDetailDictionaryIndicatorView> ProjectCategoryDetailDictionaryIndicators { get { return Set<ProjectCategoryDetailDictionaryIndicatorView>().AsNoTracking(); } }
        public DbQuery<ProjectCategoryDetailSimulationCombinationView> ProjectCategoryDetailDictionaryCombinations { get { return Set<ProjectCategoryDetailSimulationCombinationView>().AsNoTracking(); } }
        public DbQuery<ProjectRoleDictionaryLevelView> ProjectRoleDictionaryLevels { get { return Set<ProjectRoleDictionaryLevelView>().AsNoTracking(); } }
        public DbQuery<ProjectCategoryDetailCompetenceSimulationView> ProjectCategoryDetailCompetenceSimulations { get { return Set<ProjectCategoryDetailCompetenceSimulationView>().AsNoTracking(); } }
        public DbQuery<ProjectRoleView> ProjectRoles { get { return Set<ProjectRoleView>().AsNoTracking(); } }
        public DbQuery<ProjectCandidateView> ProjectCandidates { get { return Set<ProjectCandidateView>().AsNoTracking(); } }
        public DbQuery<ProjectPlanView> ProjectPlans { get { return Set<ProjectPlanView>().AsNoTracking(); } }
        public DbQuery<ProjectPlanPhaseView> ProjectPlanPhases { get { return Set<ProjectPlanPhaseView>().AsNoTracking(); } }
        public DbQuery<ProjectPlanPhaseEntryView> ProjectPlanPhaseEntries { get { return Set<ProjectPlanPhaseEntryView>().AsNoTracking(); } }
        public DbQuery<ProjectPriceIndexView> ProjectPriceIndices { get { return Set<ProjectPriceIndexView>().AsNoTracking(); } }
        public DbQuery<TimesheetEntryView> TimesheetEntries { get { return Set<TimesheetEntryView>().AsNoTracking(); } }
        public DbQuery<ProjectFixedPriceView> ProjectFixedPrices { get { return Set<ProjectFixedPriceView>().AsNoTracking(); } }
        public DbQuery<ProductsheetEntryView> ProductsheetEntries { get { return Set<ProductsheetEntryView>().AsNoTracking(); } }
        public DbQuery<SubProjectView> SubProjects { get { return Set<SubProjectView>().AsNoTracking(); } }
        public DbQuery<MainProjectView> MainProjects { get { return Set<MainProjectView>().AsNoTracking(); } }
        public DbQuery<ProjectCandidateIndicatorSimulationScoreView> ProjectCandidateIndicatorSimulationScores { get { return Set<ProjectCandidateIndicatorSimulationScoreView>().AsNoTracking(); } }
        public DbQuery<ProjectCandidateCompetenceSimulationScoreView> ProjectCandidateCompetenceSimulationScores { get { return Set<ProjectCandidateCompetenceSimulationScoreView>().AsNoTracking(); } }
        public DbQuery<ProjectCandidateIndicatorSimulationFocusedScoreView> ProjectCandidateIndicatorSimulationFocusedScores { get { return Set<ProjectCandidateIndicatorSimulationFocusedScoreView>().AsNoTracking(); } }
        public DbQuery<ProjectCandidateClusterScoreView> ProjectCandidateClusterScores { get { return Set<ProjectCandidateClusterScoreView>().AsNoTracking(); } }
        public DbQuery<ProjectCandidateCompetenceScoreView> ProjectCandidateCompetenceScores { get { return Set<ProjectCandidateCompetenceScoreView>().AsNoTracking(); } }
        public DbQuery<ProjectCandidateIndicatorScoreView> ProjectCandidateIndicatorScores { get { return Set<ProjectCandidateIndicatorScoreView>().AsNoTracking(); } }
        public DbQuery<ProjectCandidateResumeView> ProjectCandidateResumes { get { return Set<ProjectCandidateResumeView>().AsNoTracking(); } }
        public DbQuery<ProjectCandidateResumeFieldView> ProjectCandidateResumeFields { get { return Set<ProjectCandidateResumeFieldView>().AsNoTracking(); } }
        public DbQuery<AdviceView> Advices { get { return Set<AdviceView>().AsNoTracking(); } }
        public DbQuery<ProjectCandidateDetailView> ProjectCandidateDetails { get { return Set<ProjectCandidateDetailView>().AsNoTracking(); } }
        public DbQuery<ProposalView> Proposals { get { return Set<ProposalView>().AsNoTracking(); } }
        public DbQuery<ProjectTypeCategoryUnitPriceView> ProjectTypeCategoryUnitPrices { get { return Set<ProjectTypeCategoryUnitPriceView>().AsNoTracking(); } }
        public DbQuery<ProjectTypeCategoryLevelView> ProjectTypeCategoryLevels { get { return Set<ProjectTypeCategoryLevelView>().AsNoTracking(); } }
        public DbQuery<FrameworkAgreementView> FrameworkAgreements { get { return Set<FrameworkAgreementView>().AsNoTracking(); } }
        public DbQuery<ProjectCandidateReportRecipientView> ProjectCandidateReportRecipients { get { return Set<ProjectCandidateReportRecipientView>().AsNoTracking(); } }
        public DbQuery<ProjectDocumentMetadataView> ProjectDocumentMetadatas { get { return Set<ProjectDocumentMetadataView>().AsNoTracking(); } }
        public DbQuery<ProjectCandidateCategoryDetailTypeView> ProjectCandidateCategoryDetailTypes { get { return Set<ProjectCandidateCategoryDetailTypeView>().AsNoTracking(); } }

        public DbQuery<ProjectCandidateProjectView> ProjectCandidateProjects { get { return Set<ProjectCandidateProjectView>().AsNoTracking(); } }

        public DbQuery<ReportStatusView> ReportStatuses { get { return Set<ReportStatusView>().AsNoTracking(); } }
        public DbQuery<ProjectDnaView> ProjectDnas { get { return Set<ProjectDnaView>().AsNoTracking(); } }
        public DbQuery<ProjectProductView> ProjectProducts { get { return Set<ProjectProductView>().AsNoTracking(); } }
        public DbQuery<ProjectEvaluationView> ProjectEvaluations { get { return Set<ProjectEvaluationView>().AsNoTracking(); } }
        public DbQuery<EvaluationFormView> EvaluationForms { get { return Set<EvaluationFormView>().AsNoTracking(); } }
        public DbQuery<EvaluationFormAcdcView> EvaluationFormsAcdc { get { return Set<EvaluationFormAcdcView>().AsNoTracking(); } }
        public DbQuery<EvaluationFormCoachingView> EvaluationFormsCoaching { get { return Set<EvaluationFormCoachingView>().AsNoTracking(); } }
        public DbQuery<EvaluationFormCustomProjectsView> EvaluationFormsCustomProjects { get { return Set<EvaluationFormCustomProjectsView>().AsNoTracking(); } }
        public DbQuery<MailStatusTypeView> MailStatusTypes { get { return Set<MailStatusTypeView>().AsNoTracking(); } }
        public DbQuery<EvaluationFormTypeView> EvaluationFormTypes { get { return Set<EvaluationFormTypeView>().AsNoTracking(); } }
        public DbQuery<ProjectComplaintView> ProjectComplaints { get { return Set<ProjectComplaintView>().AsNoTracking(); } }
        public DbQuery<ComplaintTypeView> ComplaintTypes { get { return Set<ComplaintTypeView>().AsNoTracking(); } }
        public DbQuery<ProjectReportRecipientView> ProjectReportRecipients { get { return Set<ProjectReportRecipientView>().AsNoTracking(); } }
        public DbQuery<TheoremListRequestView> TheoremListRequests { get { return Set<TheoremListRequestView>().AsNoTracking(); } }
        public DbQuery<TheoremListView> TheoremLists { get { return Set<TheoremListView>().AsNoTracking(); } }
        public DbQuery<TheoremListTemplateView> TheoremListTemplates { get { return Set<TheoremListTemplateView>().AsNoTracking(); } }

        public IEnumerable<RecentProjectView> ListRecentProjects(Guid userId, int maxProjects)
        {
            var query = Database.SqlQuery<RecentProjectView>("Project_ListRecentProjects {0}", userId);
            return query.Take(maxProjects);
        }

        public IEnumerable<SearchCrmProjectResultItemView> SearchCrmProjects(Guid? projectId, string projectName, bool withPlannedStatus, bool withRunningStatus, bool withDoneStatus, bool withStoppedStatus)
        {
            var query = Database.SqlQuery<SearchCrmProjectResultItemView>("Project_SearchCrmProjects {0}, {1}, {2}, {3}, {4}, {5}", projectId, projectName, withPlannedStatus, withRunningStatus, withDoneStatus, withStoppedStatus);
            return query;
        }

        public IEnumerable<int> ListProposalYears()
        {
            var query = Database.SqlQuery<int>("Proposal_ListYears");
            return query;
        }

        public IEnumerable<int> ListFrameworkAgreementYears()
        {
            var query = Database.SqlQuery<int>("FrameworkAgreement_ListYears");
            return query;
        }

        public IEnumerable<ProjectDnaSelectedTypeView> ListProjectDnaTypes(Guid projectDnaId)
        {
            var query = Database.SqlQuery<ProjectDnaSelectedTypeView>("ProjectDna_ListProjectDnaTypes {0}", projectDnaId).OrderByDescending(e => e.IsSelected).ThenBy(e => e.Name);
            return query;
        }

        public IEnumerable<ProjectDnaSelectedContactPersonView> ListProjectDnaContactPersons(Guid projectDnaId)
        {
            var query = Database.SqlQuery<ProjectDnaSelectedContactPersonView>("ProjectDna_ListProjectDnaContactPersons {0}", projectDnaId).OrderByDescending(e => e.IsSelected).ThenBy(e => e.IsRetired).ThenBy(e => e.FullName);
            return query;
        }

        public IEnumerable<NeopirScoreView> ListNeopirScores(Guid projectCandidateId)
        {
            var query = Database.SqlQuery<NeopirScoreView>("External_ListNeopirScores {0}", projectCandidateId);
            return query;
        }

        public IEnumerable<LeaderScoreView> ListLeiderschapScores(Guid projectCandidateId)
        {
            var query = Database.SqlQuery<LeaderScoreView>("External_ListLeiderschapScores {0}", projectCandidateId);
            return query;
        }

        public IEnumerable<ProjectCategoryDetailDictionaryIndicatorView> ListProjectCategoryDetailDictionaryIndicators(Guid projectCategoryDetailId, int? languageId = null)
        {
            var query = Database.SqlQuery<ProjectCategoryDetailDictionaryIndicatorView>("ProjectCategoryDetail_ListDictionaryIndicators {0}, {1}", projectCategoryDetailId, languageId);
            return query;
        }

        public IEnumerable<ProjectCandidateOverviewEntryView> RetrieveProjectCandidateReservedAssistantOverview(DateTime? startDate = null, DateTime? endDate = null, Guid? customerAssistantId = null)
        {
            var query = Database.SqlQuery<ProjectCandidateOverviewEntryView>("ProjectCandidateReserved_AssistantOverview {0}, {1}, {2}", startDate, endDate, customerAssistantId);
            return query;
        }

        public IEnumerable<ProjectCandidateOverviewEntryView> RetrieveProjectCandidateCategoryType1AssistantOverview(DateTime? startDate = null, DateTime? endDate = null, Guid? projectCandidateCategoryDetailTypeId = null, Guid? customerAssistantId = null)
        {
            var query = Database.SqlQuery<ProjectCandidateOverviewEntryView>("ProjectCandidateCategoryType1_AssistantOverview {0}, {1}, {2}, {3}", projectCandidateCategoryDetailTypeId, startDate, endDate, customerAssistantId);
            return query;
        }

        public IEnumerable<ProjectCandidateOverviewEntryView> RetrieveProjectCandidateCategoryType2AssistantOverview(DateTime? startDate = null, DateTime? endDate = null, Guid? projectCandidateCategoryDetailTypeId = null, Guid? customerAssistantId = null)
        {
            var query = Database.SqlQuery<ProjectCandidateOverviewEntryView>("ProjectCandidateCategoryType2_AssistantOverview {0}, {1}, {2}, {3}", projectCandidateCategoryDetailTypeId, startDate, endDate, customerAssistantId);
            return query;
        }

        public IEnumerable<ProjectCandidateOverviewEntryView> RetrieveProjectCandidateCategoryType3AssistantOverview(DateTime? startDate, DateTime? endDate = null,
                                                                                  Guid? projectCandidateCategoryDetailTypeId = null,
                                                                                  Guid? customerAssistantId = null)
        {
            var query = Database.SqlQuery<ProjectCandidateOverviewEntryView>("ProjectCandidateCategoryType3_AssistantOverview {0}, {1}, {2}, {3}", projectCandidateCategoryDetailTypeId, startDate, endDate, customerAssistantId);
            return query;
        }

        public IEnumerable<ProjectCandidateOverviewEntryView> RetrieveProjectCandidateCancelledAssistantOverview(DateTime? startDate, DateTime? endDate = null, Guid? customerAssistantId = null)
        {
            var query = Database.SqlQuery<ProjectCandidateOverviewEntryView>("ProjectCandidateCancelled_AssistantOverview {0}, {1}, {2}", startDate, endDate, customerAssistantId);
            return query;
        }

        public IEnumerable<ProjectCandidateReportingOverviewEntryView> RetrieveProjectCandidateReportingAssistantOverview(DateTime? startDate = null, Guid? projectCandidateId = null, Guid? customerAssistantId = null)
        {
            var query = Database.SqlQuery<ProjectCandidateReportingOverviewEntryView>("ProjectCandidate_ReportingOverview {0}, {1}, {2}", projectCandidateId, startDate, customerAssistantId);
            return query;
        }

        public IEnumerable<ProjectInvoiceAmountOverviewEntryView> ListProjectInvoiceAmountOverviewEntries(Guid id)
        {
            var query = Database.SqlQuery<ProjectInvoiceAmountOverviewEntryView>("Project_InvoiceAmountOverview {0}", id);
            return query;
        }

        public IEnumerable<SimulationContextLoginView> ListSimulationContextLogins(Guid projectCandidateId)
        {
            var query = Database.SqlQuery<SimulationContextLoginView>("ProjectCandidate_ListSimulationContextLogins {0}", projectCandidateId);
            return query;
        }

        public IEnumerable<ProjectRoleDictionaryLevelView> ListProjectRoleDictionaryLevels(Guid projectRoleId, int? languageId = null)
        {
            var query = Database.SqlQuery<ProjectRoleDictionaryLevelView>("ProjectCategoryDetail_ListProjectRoleDictionaryLevels {0}, {1}", projectRoleId, languageId);
            return query;
        }

        public IEnumerable<ProjectCandidateOverviewEntryView> RetrieveProjectCandidateAssistantOverview(DateTime? startDate = null, DateTime? endDate = null, Guid? projectCandidateId = null, Guid? customerAssistantId = null)
        {
            var query = Database.SqlQuery<ProjectCandidateOverviewEntryView>("ProjectCandidate_AssistantOverview {0}, {1}, {2}, {3}", projectCandidateId, startDate, endDate, customerAssistantId);
            return query;
        }

        public IEnumerable<ProjectCandidateReportRecipientView> ListProjectCandidateReportRecipients(Guid projectCandidateId)
        {
            var query = Database.SqlQuery<ProjectCandidateReportRecipientView>("ProjectCandidate_ListReportRecipients {0}", projectCandidateId);
            return query;
        }
    }
}
