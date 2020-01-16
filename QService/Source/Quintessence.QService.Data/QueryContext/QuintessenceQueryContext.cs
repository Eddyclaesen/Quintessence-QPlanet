using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Quintessence.QService.Core.Configuration;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.QueryModel.Cam;
using Quintessence.QService.QueryModel.Crm;
using Quintessence.QService.QueryModel.Dim;
using Quintessence.QService.QueryModel.Inf;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Rep;
using Quintessence.QService.QueryModel.Scm;
using Quintessence.QService.QueryModel.Sec;
using Quintessence.QService.QueryModel.Sim;
using Quintessence.QService.QueryModel.Sof;
using Quintessence.QService.QueryModel.Wsm;

namespace Quintessence.QService.Data.QueryContext
{
    /// <summary>
    /// Quintessence data context
    /// </summary>
    public partial class QuintessenceQueryContext : DbContext, IQuintessenceQueryContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuintessenceQueryContext"/> class.
        /// </summary>
        public QuintessenceQueryContext(IConfiguration configuration)
            : base(configuration.GetConnectionStringConfiguration<IQuintessenceQueryContext>())
        {
        }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<SimulationCombinationLanguageView>().HasKey(sl => new { sl.LanguageId, sl.SimulationCombinationId });
            modelBuilder.Entity<ProjectCandidateProjectView>().HasKey(sl => new { sl.ProjectCandidateId, sl.SubProjectId });
            modelBuilder.Entity<CrmAssessorAppointmentView>().HasKey(sl => new { sl.Id });
            modelBuilder.Entity<ProjectDocumentMetadataView>().HasKey(sl => new { sl.ProjectId, sl.DocumentUniqueId });

            modelBuilder.Entity<CrmUnregisteredCandidateAppointmentView>().HasKey(sl => new { sl.Id });

            modelBuilder.Entity<CrmAssociateView>().Ignore(cav => cav.User);
            
            modelBuilder.Entity<ActivityDetailTrainingView>().Ignore(adt => adt.ActivityDetailTrainingCandidates);

            //Register inheritance (TPT: Table per Type)
            
            modelBuilder.Entity<EvaluationFormAcdcView>().ToTable(typeof(EvaluationFormAcdcView).Name);
            modelBuilder.Entity<EvaluationFormCoachingView>().ToTable(typeof(EvaluationFormCoachingView).Name);
            modelBuilder.Entity<EvaluationFormCustomProjectsView>().ToTable(typeof(EvaluationFormCustomProjectsView).Name);

            modelBuilder.Entity<ProjectCategoryAcDetailView>().ToTable(typeof(ProjectCategoryAcDetailView).Name);
            modelBuilder.Entity<ProjectCategoryFaDetailView>().ToTable(typeof(ProjectCategoryFaDetailView).Name);
            modelBuilder.Entity<ProjectCategoryDcDetailView>().ToTable(typeof(ProjectCategoryDcDetailView).Name);
            modelBuilder.Entity<ProjectCategoryFdDetailView>().ToTable(typeof(ProjectCategoryFdDetailView).Name);
            modelBuilder.Entity<ProjectCategoryEaDetailView>().ToTable(typeof(ProjectCategoryEaDetailView).Name);
            modelBuilder.Entity<ProjectCategoryPsDetailView>().ToTable(typeof(ProjectCategoryPsDetailView).Name);
            modelBuilder.Entity<ProjectCategorySoDetailView>().ToTable(typeof(ProjectCategorySoDetailView).Name);
            modelBuilder.Entity<ProjectCategoryCaDetailView>().ToTable(typeof(ProjectCategoryCaDetailView).Name);
            modelBuilder.Entity<ProjectCategoryCuDetailView>().ToTable(typeof(ProjectCategoryCuDetailView).Name);

            modelBuilder.Entity<ProjectCategoryDetailType1View>().ToTable(typeof(ProjectCategoryDetailType1View).Name);
            modelBuilder.Entity<ProjectCategoryDetailType2View>().ToTable(typeof(ProjectCategoryDetailType2View).Name);
            modelBuilder.Entity<ProjectCategoryDetailType3View>().ToTable(typeof(ProjectCategoryDetailType3View).Name);

            modelBuilder.Entity<ProjectCandidateCategoryDetailType1View>().ToTable(typeof(ProjectCandidateCategoryDetailType1View).Name);
            modelBuilder.Entity<ProjectCandidateCategoryDetailType2View>().ToTable(typeof(ProjectCandidateCategoryDetailType2View).Name);
            modelBuilder.Entity<ProjectCandidateCategoryDetailType3View>().ToTable(typeof(ProjectCandidateCategoryDetailType3View).Name);

            modelBuilder.Entity<ProjectPlanPhaseActivityView>().ToTable(typeof(ProjectPlanPhaseActivityView).Name);
            modelBuilder.Entity<ProjectPlanPhaseProductView>().ToTable(typeof(ProjectPlanPhaseProductView).Name);

            modelBuilder.Entity<SimulationMatrixEntryView>().ToTable(typeof(SimulationMatrixEntryView).Name);

            modelBuilder.Entity<ActivityDetailView>().ToTable(typeof(ActivityDetailView).Name);
            modelBuilder.Entity<ActivityDetailCoachingView>().ToTable(typeof(ActivityDetailCoachingView).Name);
            modelBuilder.Entity<ActivityDetailConsultingView>().ToTable(typeof(ActivityDetailConsultingView).Name);
            modelBuilder.Entity<ActivityDetailSupportView>().ToTable(typeof(ActivityDetailSupportView).Name);
            modelBuilder.Entity<ActivityDetailTrainingView>().ToTable(typeof(ActivityDetailTrainingView).Name);
            modelBuilder.Entity<ActivityDetailWorkshopView>().ToTable(typeof(ActivityDetailWorkshopView).Name);

            modelBuilder.Entity<EmployeeView>().ToTable(typeof(EmployeeView).Name);

            modelBuilder.Entity<ProjectTypeView>();
            modelBuilder.Entity<ProjectView>().HasMany(p => p.CrmProjects).WithMany(cp => cp.Projects).Map(p => p.ToTable("Project2CrmProjectView").MapLeftKey("ProjectId").MapRightKey("CrmProjectId"));
            modelBuilder.Entity<AssessmentDevelopmentProjectView>().ToTable(typeof(AssessmentDevelopmentProjectView).Name);
            modelBuilder.Entity<ConsultancyProjectView>().ToTable(typeof(ConsultancyProjectView).Name);
            modelBuilder.Entity<ProjectTypeCategoryView>();
            modelBuilder.Entity<ProjectTypeCategoryDefaultValueView>();
            modelBuilder.Entity<ProjectCategoryDetailView>();
            modelBuilder.Entity<ProjectCategoryDetailDictionaryIndicatorView>();
            modelBuilder.Entity<ProjectCategoryDetailSimulationCombinationView>();
            modelBuilder.Entity<ProjectRoleView>();
            modelBuilder.Entity<ProjectRoleDictionaryLevelView>();
            modelBuilder.Entity<ProjectCategoryDetailCompetenceSimulationView>();
            modelBuilder.Entity<ProjectCandidateView>().Ignore(pc => pc.TheoremListRequests); ;
            modelBuilder.Entity<ProjectPlanView>();
            modelBuilder.Entity<ProjectPlanPhaseView>();
            modelBuilder.Entity<ProjectPlanPhaseEntryView>();
            modelBuilder.Entity<ProjectPriceIndexView>();
            modelBuilder.Entity<TimesheetEntryView>();
            modelBuilder.Entity<ProjectFixedPriceView>();
            modelBuilder.Entity<ProductsheetEntryView>();
            modelBuilder.Entity<SubProjectView>();
            modelBuilder.Entity<MainProjectView>();
            modelBuilder.Entity<ProjectCandidateIndicatorSimulationScoreView>();
            modelBuilder.Entity<ProjectCandidateCompetenceSimulationScoreView>();
            modelBuilder.Entity<ProjectCandidateIndicatorSimulationFocusedScoreView>();
            modelBuilder.Entity<ProjectCandidateClusterScoreView>();
            modelBuilder.Entity<ProjectCandidateCompetenceScoreView>();
            modelBuilder.Entity<ProjectCandidateIndicatorScoreView>();
            modelBuilder.Entity<ProjectCandidateResumeView>();
            modelBuilder.Entity<ProjectCandidateResumeFieldView>();
            modelBuilder.Entity<AdviceView>();
            modelBuilder.Entity<ProjectCandidateDetailView>();
            modelBuilder.Entity<ProposalView>();
            modelBuilder.Entity<ProjectTypeCategoryUnitPriceView>();
            modelBuilder.Entity<ProjectTypeCategoryLevelView>();
            modelBuilder.Entity<FrameworkAgreementView>();
            modelBuilder.Entity<ProjectCandidateReportRecipientView>();
            modelBuilder.Entity<ProjectDocumentMetadataView>();
            modelBuilder.Entity<ProjectCandidateCategoryDetailTypeView>();
            modelBuilder.Entity<ReportStatusView>();
            modelBuilder.Entity<ProjectDnaView>();
            modelBuilder.Entity<ProjectProductView>();
            modelBuilder.Entity<ProjectEvaluationView>();
            modelBuilder.Entity<EvaluationFormView>();
            modelBuilder.Entity<EvaluationFormAcdcView>();
            modelBuilder.Entity<EvaluationFormCoachingView>();
            modelBuilder.Entity<EvaluationFormCustomProjectsView>();
            modelBuilder.Entity<MailStatusTypeView>();
            modelBuilder.Entity<EvaluationFormTypeView>();
            modelBuilder.Entity<ProjectComplaintView>();
            modelBuilder.Entity<ComplaintTypeView>();
            modelBuilder.Entity<ProjectReportRecipientView>();
            modelBuilder.Entity<TheoremListRequestView>();
            modelBuilder.Entity<TheoremListView>();
            modelBuilder.Entity<TheoremListTemplateView>();
            modelBuilder.Entity<ProjectRevenueDistributionView>();

            modelBuilder.Entity<CandidateView>();
            modelBuilder.Entity<ProgramComponentView>();
            modelBuilder.Entity<ProgramComponentSpecialView>();

            modelBuilder.Entity<CrmContactView>();
            modelBuilder.Entity<CrmAssociateView>();
            modelBuilder.Entity<CrmActiveProjectView>();
            modelBuilder.Entity<CrmProjectView> ();
            modelBuilder.Entity<ContactDetailView> ();
            modelBuilder.Entity<CrmTimesheetUnregisteredEntryView>();
            modelBuilder.Entity<CrmAppointmentView> ();
            modelBuilder.Entity<CrmEmailView> ();
            modelBuilder.Entity<CrmUnsynchronizedEmployeeView>();

            modelBuilder.Entity<DictionaryView> ();
            modelBuilder.Entity<DictionaryIndicatorMatrixEntryView>();
            modelBuilder.Entity<DictionaryIndicatorView> ();
            modelBuilder.Entity<DictionaryLevelView> ();
            modelBuilder.Entity<DictionaryAdminView> ();
            modelBuilder.Entity<DictionaryClusterAdminView>();
            modelBuilder.Entity<DictionaryCompetenceAdminView> ();
            modelBuilder.Entity<DictionaryLevelAdminView> ();
            modelBuilder.Entity<DictionaryIndicatorAdminView>();
            modelBuilder.Entity<DictionaryClusterTranslationView>();

            modelBuilder.Entity<LanguageView>();
            modelBuilder.Entity<OfficeView>();
            modelBuilder.Entity<AssessmentRoomView>();
            modelBuilder.Entity<AssessorColorView>();
            modelBuilder.Entity<MailTemplateView>();
            modelBuilder.Entity<MailTemplateTranslationView>();
            modelBuilder.Entity<JobDefinitionView> ();
            modelBuilder.Entity<JobScheduleView> ();
            modelBuilder.Entity<JobView> ();

            modelBuilder.Entity<CandidateReportDefinitionView> ();
            modelBuilder.Entity<CandidateReportDefinitionFieldView>();
            modelBuilder.Entity<CandidateScoreReportTypeView> ();
            modelBuilder.Entity<ReportTypeView> ();
            modelBuilder.Entity<ReportDefinitionView> ();
            modelBuilder.Entity<ReportParameterView> ();

            modelBuilder.Entity<ActivityTypeProfileView> ();
            modelBuilder.Entity<ActivityView> ();
            modelBuilder.Entity<ActivityTypeView> ();
            modelBuilder.Entity<ActivityProfileView> ();
            modelBuilder.Entity<ProductView> ();
            modelBuilder.Entity<ProductTypeView> ();
            modelBuilder.Entity<ActivityDetailView> (); 
            modelBuilder.Entity<TrainingTypeView> ();
            modelBuilder.Entity<ProfileView> ();
            modelBuilder.Entity<ActivityDetailTrainingCandidateView>();

            modelBuilder.Entity<UserView> ();
            modelBuilder.Entity<RoleView> ();
            modelBuilder.Entity<AuthenticationTokenView>();
            modelBuilder.Entity<RoleOperationView> ();

            modelBuilder.Entity<SimulationSetView> ();
            modelBuilder.Entity<SimulationDepartmentView> ();
            modelBuilder.Entity<SimulationLevelView> ();
            modelBuilder.Entity<SimulationView> ();
            modelBuilder.Entity<SimulationTranslationView>();
            modelBuilder.Entity<SimulationMatrixEntryView>();
            modelBuilder.Entity<SimulationCombinationView>();
            modelBuilder.Entity<SimulationContextView> ();
            modelBuilder.Entity<SimulationContextUserView>();

            modelBuilder.Entity<UserProfileView>();
        }
    }
}
