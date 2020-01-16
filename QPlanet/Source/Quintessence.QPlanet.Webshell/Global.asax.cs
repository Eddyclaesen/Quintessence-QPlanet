using System;
using System.Diagnostics;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Quintessence.QPlanet.Infrastructure.Logging;
using Quintessence.QPlanet.Infrastructure.Web;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QPlanet.ViewModel.Cam;
using Quintessence.QPlanet.ViewModel.Crm;
using Quintessence.QPlanet.ViewModel.Dim;
using Quintessence.QPlanet.ViewModel.Fin;
using Quintessence.QPlanet.ViewModel.Inf;
using Quintessence.QPlanet.ViewModel.Prm;
using Quintessence.QPlanet.ViewModel.Prm.ProjectCategoryDetail;
using Quintessence.QPlanet.ViewModel.Rep;
using Quintessence.QPlanet.ViewModel.Scm;
using Quintessence.QPlanet.ViewModel.Sec;
using Quintessence.QPlanet.ViewModel.Sim;
using Quintessence.QPlanet.Webshell.Areas.Admin.Models.AdminProject;
using Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectDetailControllerBase;
using Quintessence.QPlanet.Webshell.Infrastructure.ActionFilters;
using Quintessence.QPlanet.Webshell.Infrastructure.ModelBinders;
using Quintessence.QPlanet.Webshell.Infrastructure.RouteHandler;
using Quintessence.QPlanet.Webshell.Infrastructure.TypeConverters;
using Quintessence.QPlanet.Webshell.Models.Shared;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CandidateManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.CustomerRelationshipManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.InfrastructureManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SimulationManagement;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.SupplyChainManagement;
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

namespace Quintessence.QPlanet.Webshell
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            routes.MapRoute(
                name: "Reporting",
                url: "Reporting/Image/{report}/{parameter}/{value}/{file}.{format}",
                defaults: new { controller = "Reporting", action = "Image" }
            );

            //Route to reporting services
            routes.Add("ReportRoute", new Route("reports/{reportname}", new ReportRouteHandler()));

            routes.MapRoute(
                name: "Search",
                url: "General/Search/{term}",
                defaults: new { controller = "General", action = "Search" }
            );

            routes.MapRoute(
                name: "Help",
                url: "General/Help/{location}",
                defaults: new { controller = "General", action = "Help" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        protected void Application_Start()
        {
            HtmlHelper.ClientValidationEnabled = true;
            HtmlHelper.UnobtrusiveJavaScriptEnabled = true;

            //Remove all engines and add razor since it is the only one in use.
            //Under App_Start\RazorGeneratorMvcStart the precompiled engine is inserted
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine { FileExtensions = new[] { "cshtml" } });

            AreaRegistration.RegisterAllAreas();

            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(EditProjectCandidateCategoryDetailTypeModel), new ProjectCandidateCategoryDetailTypeModelBinder());
            ModelBinders.Binders.Add(typeof(BaseEditInvoiceModel), new BaseEditInvoiceModelBinder());
            ModelBinders.Binders.Add(typeof(EditInvoicingBaseEntryModel), new EditInvoicingEntryModelModelBinder());

            GlobalFilters.Filters.Add(new QPlanetHandleTempdataErrorAttribute());

            //Register the ConditionalRequiredAttribute for client-side validation
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(ConditionalRequiredAttribute), typeof(RequiredAttributeAdapter));

            InitializeAutoMapper();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            BundleTable.Bundles.RegisterTemplateBundles();
        }

        /// <summary>
        /// Handles the BeginRequest event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            try
            {
                HttpContext.Current.Items["DurationLog"] = DurationLog.Create();
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
            }
        }

        /// <summary>
        /// Handles the EndRequest event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            try
            {
                var durationLog = HttpContext.Current.Items["DurationLog"] as DurationLog;

                if (durationLog == null)
                    return;

                if (HttpContext.Current.Request.Path.StartsWith("/Content", StringComparison.OrdinalIgnoreCase)
                    || HttpContext.Current.Request.Path.StartsWith("/Scripts", StringComparison.OrdinalIgnoreCase)
                    || HttpContext.Current.Request.Path.StartsWith("/Images", StringComparison.OrdinalIgnoreCase))
                    return;

                durationLog.Dispose("Application.EndRequest", HttpContext.Current.Request.Path);
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
            }
        }

        private void InitializeAutoMapper()
        {
            #region Shared
            Mapper.CreateMap<DataTableParameterModel, DataTablePaging>().ConvertUsing<DataTableParameterModelConverter>();
            #endregion

            #region DictionaryManagement mappings
            //DictionaryManagement - Query
            Mapper.CreateMap(typeof(DictionaryView), typeof(DictionaryModel));
            Mapper.CreateMap(typeof(DictionaryClusterView), typeof(DictionaryClusterModel));
            Mapper.CreateMap(typeof(DictionaryCompetenceView), typeof(DictionaryCompetenceModel));
            Mapper.CreateMap(typeof(DictionaryLevelView), typeof(DictionaryLevelModel));
            Mapper.CreateMap(typeof(DictionaryIndicatorView), typeof(DictionaryIndicatorModel));
            Mapper.CreateMap<AvailableDictionaryView, DictionarySelectListItemModel>();

            Mapper.CreateMap(typeof(DictionaryView), typeof(EditDictionaryLanguageModel));
            Mapper.CreateMap(typeof(DictionaryAdminView), typeof(EditDictionaryModel));
            Mapper.CreateMap(typeof(DictionaryClusterAdminView), typeof(EditDictionaryClusterModel));
            Mapper.CreateMap(typeof(DictionaryClusterTranslationAdminView), typeof(EditDictionaryClusterTranslationModel));
            Mapper.CreateMap(typeof(DictionaryCompetenceAdminView), typeof(EditDictionaryCompetenceModel));
            Mapper.CreateMap(typeof(DictionaryCompetenceTranslationAdminView), typeof(EditDictionaryCompetenceTranslationModel));
            Mapper.CreateMap(typeof(DictionaryLevelAdminView), typeof(EditDictionaryLevelModel));
            Mapper.CreateMap(typeof(DictionaryLevelTranslationAdminView), typeof(EditDictionaryLevelTranslationModel));
            Mapper.CreateMap(typeof(DictionaryIndicatorAdminView), typeof(EditDictionaryIndicatorModel));
            Mapper.CreateMap(typeof(DictionaryIndicatorTranslationAdminView), typeof(EditDictionaryIndicatorTranslationModel));

            Mapper.CreateMap<DictionaryImportView, EditImportDictionaryModel>();
            Mapper.CreateMap<DictionaryClusterImportView, EditImportDictionaryClusterModel>();
            Mapper.CreateMap<DictionaryClusterTranslationImportView, EditImportDictionaryClusterTranslationModel>();
            Mapper.CreateMap<DictionaryCompetenceImportView, EditImportDictionaryCompetenceModel>();
            Mapper.CreateMap<DictionaryCompetenceTranslationImportView, EditImportDictionaryCompetenceTranslationModel>();
            Mapper.CreateMap<DictionaryLevelImportView, EditImportDictionaryLevelModel>();
            Mapper.CreateMap<DictionaryLevelTranslationImportView, EditImportDictionaryLevelTranslationModel>();
            Mapper.CreateMap<DictionaryIndicatorImportView, EditImportDictionaryIndicatorModel>();
            Mapper.CreateMap<DictionaryIndicatorTranslationImportView, EditImportDictionaryIndicatorTranslationModel>();

            //DictionaryManagement - Command
            Mapper.CreateMap<CreateNewDictionaryClusterModel, CreateNewDictionaryClusterRequest>();
            Mapper.CreateMap<CreateNewDictionaryCompetenceModel, CreateNewDictionaryCompetenceRequest>();
            Mapper.CreateMap<CreateNewDictionaryLevelModel, CreateNewDictionaryLevelRequest>();
            Mapper.CreateMap<CreateNewDictionaryIndicatorModel, CreateNewDictionaryIndicatorRequest>();

            Mapper.CreateMap<EditDictionaryModel, UpdateDictionaryRequest>();
            Mapper.CreateMap<EditDictionaryLanguageModel, UpdateDictionaryLanguageRequest>();
            Mapper.CreateMap<EditDictionaryClusterModel, UpdateDictionaryClusterRequest>();
            Mapper.CreateMap<EditDictionaryClusterTranslationModel, UpdateDictionaryClusterTranslationRequest>();
            Mapper.CreateMap<EditDictionaryCompetenceModel, UpdateDictionaryCompetenceRequest>();
            Mapper.CreateMap<EditDictionaryCompetenceTranslationModel, UpdateDictionaryCompetenceTranslationRequest>();
            Mapper.CreateMap<EditDictionaryLevelModel, UpdateDictionaryLevelRequest>();
            Mapper.CreateMap<EditDictionaryLevelTranslationModel, UpdateDictionaryLevelTranslationRequest>();
            Mapper.CreateMap<EditDictionaryIndicatorModel, UpdateDictionaryIndicatorRequest>();
            Mapper.CreateMap<EditDictionaryIndicatorTranslationModel, UpdateDictionaryIndicatorTranslationRequest>();

            Mapper.CreateMap<EditImportDictionaryModel, ImportDictionaryRequest>();
            Mapper.CreateMap<EditImportDictionaryClusterModel, ImportDictionaryClusterRequest>();
            Mapper.CreateMap<EditImportDictionaryClusterTranslationModel, ImportDictionaryClusterTranslationRequest>();
            Mapper.CreateMap<EditImportDictionaryCompetenceModel, ImportDictionaryCompetenceRequest>();
            Mapper.CreateMap<EditImportDictionaryCompetenceTranslationModel, ImportDictionaryCompetenceTranslationRequest>();
            Mapper.CreateMap<EditImportDictionaryLevelModel, ImportDictionaryLevelRequest>();
            Mapper.CreateMap<EditImportDictionaryLevelTranslationModel, ImportDictionaryLevelTranslationRequest>();
            Mapper.CreateMap<EditImportDictionaryIndicatorModel, ImportDictionaryIndicatorRequest>();
            Mapper.CreateMap<EditImportDictionaryIndicatorTranslationModel, ImportDictionaryIndicatorTranslationRequest>();
            #endregion
            
            #region SimulationManagement mappings
            //SimulationManagement - Query
            Mapper.CreateMap<SimulationTranslationView, EditSimulationTranslationModel>();
            Mapper.CreateMap<SimulationView, EditSimulationModel>();

            //SimulationManagement - Command
            Mapper.CreateMap<EditSimulationTranslationModel, UpdateSimulationTranslationRequest>();
            Mapper.CreateMap<EditSimulationModel, UpdateSimulationRequest>();
            #endregion

            #region Infrastructure mappings
            Mapper.CreateMap(typeof(MailTemplateView), typeof(EditMailTemplateModel));
            Mapper.CreateMap(typeof(MailTemplateTranslationView), typeof(EditMailTemplateTranslationModel));
            Mapper.CreateMap(typeof(EditMailTemplateModel), typeof(UpdateMailTemplateRequest));
            Mapper.CreateMap(typeof(EditMailTemplateTranslationModel), typeof(UpdateMailTemplateTranslationRequest));
            #endregion

            //SecurityManagement
            Mapper.CreateMap(typeof(UserView), typeof(UserModel));
            Mapper.CreateMap(typeof(OperationView), typeof(OperationModel));
            Mapper.CreateMap(typeof(RoleView), typeof(RoleModel));
            Mapper.CreateMap(typeof(AuthenticationTokenView), typeof(AuthenticationTokenModel));

            #region ProjectManagement mappings
            //ProjectManagement - Query
            Mapper.CreateMap<ProjectTypeView, ProjectTypeSelectListItemModel>();
            Mapper.CreateMap<AssessmentDevelopmentProjectView, EditProjectAssessmentDevelopmentModel>();
            Mapper.CreateMap<AssessmentDevelopmentProjectView, EditProjectCandidatesModel>();
            Mapper.CreateMap<ConsultancyProjectView, EditProjectConsultancyModel>();
            Mapper.CreateMap<SearchCrmProjectResultItemView, SearchCrmProjectResultItemModel>();
            Mapper.CreateMap<CrmProjectView, CrmProjectLinkModel>();
            Mapper.CreateMap<ProjectCategoryAcDetailView, EditProjectCategoryAcDetailModel>();
            Mapper.CreateMap<ProjectCategoryFaDetailView, EditProjectCategoryFaDetailModel>();
            Mapper.CreateMap<ProjectCategoryDcDetailView, EditProjectCategoryDcDetailModel>();
            Mapper.CreateMap<ProjectCategoryFdDetailView, EditProjectCategoryFdDetailModel>();
            Mapper.CreateMap<ProjectCategoryEaDetailView, EditProjectCategoryEaDetailModel>();
            Mapper.CreateMap<ProjectCategoryPsDetailView, EditProjectCategoryPsDetailModel>();
            Mapper.CreateMap<ProjectCategorySoDetailView, EditProjectCategorySoDetailModel>();
            Mapper.CreateMap<ProjectCategoryCaDetailView, EditProjectCategoryCaDetailModel>();
            Mapper.CreateMap<ProjectCategoryCuDetailView, EditProjectCategoryCuDetailModel>();
            Mapper.CreateMap<AssessmentDevelopmentProjectView, ProjectCategoryDetailProject>();
            Mapper.CreateMap<ConsultancyProjectView, ProjectCategoryDetailProject>();
            Mapper.CreateMap<ProjectRoleView, EditProjectRoleModel>();
            Mapper.CreateMap<ProjectRoleTranslationView, EditProjectRoleTranslationModel>();
            Mapper.CreateMap<ActivityView, EditActivityModel>();
            Mapper.CreateMap<ProductView, EditProductModel>();
            Mapper.CreateMap<ActivityProfileView, EditActivityProfileModel>();
            Mapper.CreateMap<ProjectPlanView, EditProjectPlanModel>();
            Mapper.CreateMap<ProjectPlanPhaseView, EditProjectPlanPhaseModel>();
            Mapper.CreateMap<ProjectPlanPhaseEntryView, EditProjectPlanPhaseEntryModel>()
                .Include<ProjectPlanPhaseActivityView, EditProjectPlanPhaseActivityModel>()
                .Include<ProjectPlanPhaseProductView, EditProjectPlanPhaseProductModel>();
            Mapper.CreateMap<ProjectPlanPhaseActivityView, EditProjectPlanPhaseActivityModel>();
            Mapper.CreateMap<ProjectPlanPhaseProductView, EditProjectPlanPhaseProductModel>();
            Mapper.CreateMap<ProjectPlanPhaseActivityView, EditProjectPlanPhaseEntryDeadlineModel>();
            Mapper.CreateMap<ProjectPlanPhaseProductView, EditProjectPlanPhaseEntryDeadlineModel>();
            Mapper.CreateMap<ProjectCategoryDetailView, EditProjectSubCategoryDetailModelBase>()
                .Include<ProjectCategoryDetailType1View, EditProjectSubCategoryDetailType1Model>()
                .Include<ProjectCategoryDetailType2View, EditProjectSubCategoryDetailType2Model>()
                .Include<ProjectCategoryDetailType3View, EditProjectSubCategoryDetailType3Model>();
            Mapper.CreateMap<ProjectCategoryDetailType1View, EditProjectSubCategoryDetailType1Model>();
            Mapper.CreateMap<ProjectCategoryDetailType2View, EditProjectSubCategoryDetailType2Model>();
            Mapper.CreateMap<ProjectCategoryDetailType3View, EditProjectSubCategoryDetailType3Model>();
            Mapper.CreateMap<ProjectTypeCategoryView, EditSubcategoryModel>();
            Mapper.CreateMap<ProjectTypeCategoryDefaultValueView, EditProjectTypeCategoryDefaultValueModel>();
            Mapper.CreateMap<ProjectCandidateView, EditProjectCandidateDetailModel>();
            Mapper.CreateMap<ProjectCandidateCategoryDetailTypeView, EditProjectCandidateCategoryDetailTypeModel>()
                .Include<ProjectCandidateCategoryDetailType1View, EditProjectCandidateCategoryDetailType1Model>()
                .Include<ProjectCandidateCategoryDetailType2View, EditProjectCandidateCategoryDetailType2Model>()
                .Include<ProjectCandidateCategoryDetailType3View, EditProjectCandidateCategoryDetailType3Model>();
            Mapper.CreateMap<ProjectCandidateCategoryDetailType1View, EditProjectCandidateCategoryDetailType1Model>();
            Mapper.CreateMap<ProjectCandidateCategoryDetailType2View, EditProjectCandidateCategoryDetailType2Model>();
            Mapper.CreateMap<ProjectCandidateCategoryDetailType3View, EditProjectCandidateCategoryDetailType3Model>();
            Mapper.CreateMap<ProjectCandidateClusterScoreView, EditProjectCandidateClusterScoreModel>();
            Mapper.CreateMap<ProjectCandidateCompetenceScoreView, EditProjectCandidateCompetenceScoreModel>();
            Mapper.CreateMap<ProjectCandidateIndicatorScoreView, EditProjectCandidateIndicatorScoreModel>();
            Mapper.CreateMap<ProjectCandidateResumeView, EditProjectCandidateResumeModel>();
            Mapper.CreateMap<ProjectCandidateResumeFieldView, EditProjectCandidateResumeFieldModel>();
            Mapper.CreateMap<ProjectTypeCategoryUnitPriceOverviewResponse, ProjectTypeCategoryUnitPricesActionModel>();
            Mapper.CreateMap<ProjectTypeCategoryUnitPriceView, EditProjectTypeCategoryUnitPriceModel>();
            Mapper.CreateMap<ProjectDnaView, EditProjectDnaModel>();
            Mapper.CreateMap<ProjectDnaSelectedTypeView, EditProjectDnaSelectedTypeModel>();
            Mapper.CreateMap<ProjectDnaSelectedContactPersonView, EditProjectDnaSelectedContactModel>();
            Mapper.CreateMap<NeopirScoreView, NeopirScoreModel>();
            Mapper.CreateMap<LeaderScoreView, LeaderScoreModel>();
            Mapper.CreateMap<ProjectDnaView, EditProjectDnaModel>();
            Mapper.CreateMap<ProjectDnaCommercialTranslationView, EditProjectDnaCommercialTranslationModel>();

            //ProjectManagement - Command
            Mapper.CreateMap<CreateProjectModel, CreateNewProjectRequest>();
            Mapper.CreateMap<EditProjectAssessmentDevelopmentModel, UpdateAssessmentDevelopmentProjectRequest>();
            Mapper.CreateMap<EditProjectConsultancyModel, UpdateConsultancyProjectRequest>();
            Mapper.CreateMap<EditProjectSubCategoryDetailType1Model, UpdateProjectCategoryDetailTypeRequest>();
            Mapper.CreateMap<EditProjectSubCategoryDetailType2Model, UpdateProjectCategoryDetailTypeRequest>();
            Mapper.CreateMap<EditProjectSubCategoryDetailType3Model, UpdateProjectCategoryDetailTypeRequest>();
            Mapper.CreateMap<EditSubcategoryModel, CreateSubcategoryRequest>();
            Mapper.CreateMap<EditProjectTypeCategoryDefaultValueModel, UpdateProjectTypeCategoryDefaultValueRequest>();
            Mapper.CreateMap<EditProjectCandidateCategoryDetailTypeModel, UpdateProjectCandidateCategoryDetailTypeRequest>()
                .Include<EditProjectCandidateCategoryDetailType1Model, UpdateProjectCandidateCategoryDetailType1Request>()
                .Include<EditProjectCandidateCategoryDetailType2Model, UpdateProjectCandidateCategoryDetailType2Request>()
                .Include<EditProjectCandidateCategoryDetailType3Model, UpdateProjectCandidateCategoryDetailType3Request>();
            Mapper.CreateMap<EditProjectCandidateCategoryDetailType1Model, UpdateProjectCandidateCategoryDetailType1Request>();
            Mapper.CreateMap<EditProjectCandidateCategoryDetailType2Model, UpdateProjectCandidateCategoryDetailType2Request>();
            Mapper.CreateMap<EditProjectCandidateCategoryDetailType3Model, UpdateProjectCandidateCategoryDetailType3Request>();
            Mapper.CreateMap<EditProjectCandidateCompetenceSimulationScoreModel, UpdateProjectCandidateCompetenceSimulationScoreRequest>();
            Mapper.CreateMap<EditProjectCandidateCompetenceSimulationFocusedScoreModel, UpdateProjectCandidateCompetenceSimulationScoreRequest>();
            Mapper.CreateMap<EditProjectCandidateIndicatorSimulationScoreModel, UpdateProjectCandidateIndicatorSimulationScoreRequest>();
            Mapper.CreateMap<EditProjectCandidateClusterScoreModel, UpdateProjectCandidateClusterScoreRequest>();
            Mapper.CreateMap<EditProjectCandidateCompetenceScoreModel, UpdateProjectCandidateCompetenceScoreRequest>();
            Mapper.CreateMap<EditProjectCandidateIndicatorScoreModel, UpdateProjectCandidateIndicatorScoreRequest>();
            Mapper.CreateMap<EditProjectCandidateResumeModel, UpdateProjectCandidateResumeRequest>();
            Mapper.CreateMap<EditProjectCandidateResumeFieldModel, UpdateProjectCandidateResumeFieldRequest>();
            Mapper.CreateMap<EditProjectRoleModel, UpdateProjectRoleRequest>();
            Mapper.CreateMap<EditProjectRoleTranslationModel, UpdateProjectRoleTranslationRequest>();
            Mapper.CreateMap<EditProjectDnaModel, UpdateProjectDnaRequest>();
            Mapper.CreateMap<EditProjectDnaCommercialTranslationModel, UpdateProjectDnaCommercialTranslationRequest>();
            #endregion

            #region SupplyChainManagement mappings
            //Query
            Mapper.CreateMap<ActivityDetailCoachingView, EditActivityDetailCoachingModel>();
            Mapper.CreateMap<ActivityDetailConsultingView, EditActivityDetailConsultingModel>();
            Mapper.CreateMap<ActivityDetailSupportView, EditActivityDetailSupportModel>();
            Mapper.CreateMap<ActivityDetailTrainingView, EditActivityDetailTrainingModel>();
            Mapper.CreateMap<ActivityDetailTrainingLanguageView, EditActivityDetailTrainingLanguageModel>();
            Mapper.CreateMap<ActivityDetailWorkshopView, EditActivityDetailWorkshopModel>();
            Mapper.CreateMap<ActivityDetailWorkshopLanguageView, EditActivityDetailWorkshopLanguageModel>();
            Mapper.CreateMap<ActivityDetailView, EditActivityDetailModel>();

            //Command
            Mapper.CreateMap<EditActivityModel, UpdateActivityRequest>();
            Mapper.CreateMap<EditActivityProfileModel, UpdateActivityProfileRequest>();
            Mapper.CreateMap<EditProductModel, UpdateProductRequest>();
            Mapper.CreateMap<EditActivityDetailCoachingModel, UpdateActivityDetailCoachingRequest>();
            Mapper.CreateMap<EditActivityDetailConsultingModel, UpdateActivityDetailConsultingRequest>();
            Mapper.CreateMap<EditActivityDetailSupportModel, UpdateActivityDetailSupportRequest>();
            Mapper.CreateMap<EditActivityDetailTrainingModel, UpdateActivityDetailTrainingRequest>();
            Mapper.CreateMap<EditActivityDetailTrainingLanguageModel, UpdateActivityDetailTrainingLanguageRequest>();
            Mapper.CreateMap<ActivityDetailTrainingLanguageView, EditActivityDetailTrainingLanguageModel>();
            Mapper.CreateMap<EditActivityDetailWorkshopModel, UpdateActivityDetailWorkshopRequest>();
            Mapper.CreateMap<EditActivityDetailWorkshopLanguageModel, UpdateActivityDetailWorkshopLanguageRequest>();
            Mapper.CreateMap<EditActivityDetailModel, UpdateActivityDetailRequest>();
            #endregion

            #region CustomerRelationshipManagement mappings
            Mapper.CreateMap<ContactDetailView, ContactDetailModel>();
            Mapper.CreateMap<ContactDetailModel, UpdateContactDetailRequest>();
            #endregion

            #region CandidateManagement mappings
            Mapper.CreateMap<CandidateView, CandidateModel>();
            Mapper.CreateMap<CandidateModel, UpdateCandidateRequest>();
            Mapper.CreateMap<CandidateModel, CreateCandidateRequest>();
            #endregion

            #region ReportManagement mappings
            //Query
            Mapper.CreateMap<ReportParameterView, EditReportParameterModel>();
            Mapper.CreateMap<ReportParameterValueView, EditReportParameterValueModel>();

            //Command
            Mapper.CreateMap<CreateNewReportParameterModel, CreateNewReportParameterRequest>();
            Mapper.CreateMap<CreateNewReportParameterValueModel, CreateNewReportParameterValueRequest>();
            Mapper.CreateMap<EditReportParameterModel, UpdateReportParameterRequest>();
            Mapper.CreateMap<EditReportParameterValueModel, UpdateReportParameterValueRequest>();
            #endregion

            Mapper.CreateMap(typeof(BaseEntityModel), typeof(DictionaryView));
            Mapper.CreateMap(typeof(BaseEntityModel), typeof(DictionaryClusterView));
            Mapper.CreateMap(typeof(BaseEntityModel), typeof(DictionaryCompetenceView));
            Mapper.CreateMap(typeof(BaseEntityModel), typeof(DictionaryLevelView));
            Mapper.CreateMap(typeof(BaseEntityModel), typeof(DictionaryIndicatorView));
            Mapper.CreateMap(typeof(BaseEntityModel), typeof(UserView));
        }
    }
}