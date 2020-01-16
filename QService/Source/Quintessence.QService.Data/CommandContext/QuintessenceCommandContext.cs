using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.Practices.Unity;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.Core.Configuration;
using Quintessence.QService.Data.Interfaces.CommandContext;
using Quintessence.QService.DataModel.Prm;
using Quintessence.QService.DataModel.Scm;
using Quintessence.QService.DataModel.Sim;

namespace Quintessence.QService.Data.CommandContext
{
    /// <summary>
    /// Quintessence data context
    /// </summary>
    public partial class QuintessenceCommandContext : DbContext, IQuintessenceCommandContext
    {
        private readonly IUnityContainer _unityContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuintessenceCommandContext"/> class.
        /// </summary>
        public QuintessenceCommandContext(IUnityContainer unityContainer, IConfiguration configuration)
            : base(configuration.GetConnectionStringConfiguration<IQuintessenceCommandContext>())
        {
            _unityContainer = unityContainer;
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

            modelBuilder.Entity<Project2CrmProject>().HasKey(p2Cp => new { p2Cp.ProjectId, p2Cp.CrmProjectId });
            modelBuilder.Entity<ProjectRole2DictionaryLevel>().HasKey(p2Cp => new { p2Cp.ProjectRoleId, p2Cp.DictionaryLevelId });
            modelBuilder.Entity<SimulationCombination2Language>().HasKey(sl => new { sl.SimulationCombinationId, sl.LanguageId });
            modelBuilder.Entity<ActivityDetailTraining2TrainingType>().HasKey(sl => new { sl.ActivityDetailTrainingId, sl.TrainingTypeId });
            modelBuilder.Entity<SubProject>().HasKey(sl => new { sl.ProjectId, sl.SubProjectId });
            modelBuilder.Entity<ProjectType2ProjectTypeCategory>().HasKey(sl => new { sl.ProjectTypeId, sl.ProjectTypeCategoryId });
            modelBuilder.Entity<ProjectRole2DictionaryIndicator>().HasKey(sl => new { sl.ProjectRoleId, sl.DictionaryIndicatorId });
            modelBuilder.Entity<ProjectDocumentMetadata>().HasKey(sl => new { sl.ProjectId, sl.DocumentUniqueId });

            //Register inheritance (TPT: Table per Type)
            modelBuilder.Entity<AssessmentDevelopmentProject>().ToTable(typeof(AssessmentDevelopmentProject).Name);
            modelBuilder.Entity<ConsultancyProject>().ToTable(typeof(ConsultancyProject).Name);
            modelBuilder.Entity<EvaluationFormAcdc>().ToTable(typeof(EvaluationFormAcdc).Name);
            modelBuilder.Entity<EvaluationFormCoaching>().ToTable(typeof(EvaluationFormCoaching).Name);
            modelBuilder.Entity<EvaluationFormCustomProjects>().ToTable(typeof(EvaluationFormCustomProjects).Name);

            modelBuilder.Entity<ProjectCategoryAcDetail>().ToTable(typeof(ProjectCategoryAcDetail).Name);
            modelBuilder.Entity<ProjectCategoryFaDetail>().ToTable(typeof(ProjectCategoryFaDetail).Name);
            modelBuilder.Entity<ProjectCategoryDcDetail>().ToTable(typeof(ProjectCategoryDcDetail).Name);
            modelBuilder.Entity<ProjectCategoryFdDetail>().ToTable(typeof(ProjectCategoryFdDetail).Name);
            modelBuilder.Entity<ProjectCategoryEaDetail>().ToTable(typeof(ProjectCategoryEaDetail).Name);
            modelBuilder.Entity<ProjectCategoryPsDetail>().ToTable(typeof(ProjectCategoryPsDetail).Name);
            modelBuilder.Entity<ProjectCategorySoDetail>().ToTable(typeof(ProjectCategorySoDetail).Name);
            modelBuilder.Entity<ProjectCategoryCaDetail>().ToTable(typeof(ProjectCategoryCaDetail).Name);
            modelBuilder.Entity<ProjectCategoryCuDetail>().ToTable(typeof(ProjectCategoryCuDetail).Name);

            modelBuilder.Entity<ProjectCategoryDetailType1>().ToTable(typeof(ProjectCategoryDetailType1).Name);
            modelBuilder.Entity<ProjectCategoryDetailType2>().ToTable(typeof(ProjectCategoryDetailType2).Name);
            modelBuilder.Entity<ProjectCategoryDetailType3>().ToTable(typeof(ProjectCategoryDetailType3).Name);

            modelBuilder.Entity<ProjectPlanPhaseActivity>().ToTable(typeof(ProjectPlanPhaseActivity).Name);
            modelBuilder.Entity<ProjectPlanPhaseProduct>().ToTable(typeof(ProjectPlanPhaseProduct).Name);

            modelBuilder.Entity<ActivityDetail>().ToTable(typeof(ActivityDetail).Name);
            modelBuilder.Entity<ActivityDetailCoaching>().ToTable(typeof(ActivityDetailCoaching).Name);
            modelBuilder.Entity<ActivityDetailConsulting>().ToTable(typeof(ActivityDetailConsulting).Name);
            modelBuilder.Entity<ActivityDetailSupport>().ToTable(typeof(ActivityDetailSupport).Name);
            modelBuilder.Entity<ActivityDetailTraining>().ToTable(typeof(ActivityDetailTraining).Name);
            modelBuilder.Entity<ActivityDetailWorkshop>().ToTable(typeof(ActivityDetailWorkshop).Name);

            //Add restrictions to Gender property of EvaluationForm, because property is a string while the column is CHAR(1) 
            modelBuilder.Entity<EvaluationForm>()
                            .Property(p => p.Gender)
                            .HasMaxLength(1)
                            .IsFixedLength()
                            .IsUnicode(false);
        }

        IUnityContainer IQuintessenceCommandContext.Container { get { return _unityContainer; } }
    }
}