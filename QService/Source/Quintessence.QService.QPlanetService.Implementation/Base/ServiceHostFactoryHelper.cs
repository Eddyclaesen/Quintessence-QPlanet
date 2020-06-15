using System;
using System.ComponentModel;
using System.Configuration;
using Microsoft.Practices.Unity;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.Business.CommandRepositories;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.Business.Interfaces.QueryRepositories;
using Quintessence.QService.Business.QueryRepositories;
using Quintessence.QService.Core.Configuration;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Data.CommandContext;
using Quintessence.QService.Data.Interfaces.CommandContext;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.Data.QueryContext;
using Quintessence.QService.DataModel.Prm;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.CommandServices;
using Quintessence.QService.QPlanetService.Implementation.QueryServices;
using Quintessence.QService.ReportingServiceData.QueryContext;
using Quintessence.QService.SharePointData.CommandContext;
using Quintessence.QService.SharePointData.QueryContext;
using ValidationContainer = Quintessence.Infrastructure.Validation.ValidationContainer;

namespace Quintessence.QService.QPlanetService.Implementation.Base
{
    public static class ServiceHostFactoryHelper
    {
        private static readonly IUnityContainer UnityContainer = new UnityContainer();

        private static ValidationRuleEngine ValidationRuleEngine
        {
            get { return UnityContainer.Resolve<ValidationRuleEngine>(); }
        }


        public static IUnityContainer CreateUnityContainer()
        {
            //var _unityContainer = new UnityContainer();
            try
            {
                UnityContainer.RegisterInstance<IConfiguration>(new Configuration());
                UnityContainer.RegisterInstance<IAzureAdB2CSettings>(new AzureAdB2CSettings
                {
                    ApplicationId = ConfigurationManager.AppSettings["AzureAdB2C.ApplicationId"],
                    B2cExtensionApplicationId = ConfigurationManager.AppSettings["AzureAdB2C.B2cExtensionApplicationId"],
                    ClientSecret = ConfigurationManager.AppSettings["AzureAdB2C.ClientSecret"],
                    TenantId = ConfigurationManager.AppSettings["AzureAdB2C.TenantId"]
                });
                UnityContainer.RegisterType<IGraphService, GraphService>();
                UnityContainer.RegisterInstance<ValidationContextLifetimeManager>(new ValidationContextLifetimeManager());
                UnityContainer.RegisterType<ValidationContainer>(UnityContainer.Resolve<ValidationContextLifetimeManager>());
                UnityContainer.RegisterInstance(new ValidationRuleEngine(UnityContainer));

                //Query contexts
                UnityContainer.RegisterType<IQuintessenceQueryContext, QuintessenceQueryContext>();
                UnityContainer.RegisterType<IDimQueryContext, QuintessenceQueryContext>();
                UnityContainer.RegisterType<IPrmQueryContext, QuintessenceQueryContext>();
                UnityContainer.RegisterType<ICrmQueryContext, QuintessenceQueryContext>();
                UnityContainer.RegisterType<ISecQueryContext, QuintessenceQueryContext>();
                UnityContainer.RegisterType<IFinQueryContext, QuintessenceQueryContext>();
                UnityContainer.RegisterType<IInfQueryContext, QuintessenceQueryContext>();
                UnityContainer.RegisterType<ISimQueryContext, QuintessenceQueryContext>();
                UnityContainer.RegisterType<ICamQueryContext, QuintessenceQueryContext>();
                UnityContainer.RegisterType<IScmQueryContext, QuintessenceQueryContext>();
                UnityContainer.RegisterType<IWsmQueryContext, QuintessenceQueryContext>();
                UnityContainer.RegisterType<IRepQueryContext, QuintessenceQueryContext>();
                UnityContainer.RegisterType<IDomQueryContext, SharePointQueryContext>();
                UnityContainer.RegisterType<IResQueryContext, ReportingServiceQueryContext>();

                //Command contexts
                UnityContainer.RegisterType<IQuintessenceCommandContext, QuintessenceCommandContext>();
                UnityContainer.RegisterType<ICamCommandContext, QuintessenceCommandContext>();
                UnityContainer.RegisterType<IDimCommandContext, QuintessenceCommandContext>();
                UnityContainer.RegisterType<IPrmCommandContext, QuintessenceCommandContext>();
                UnityContainer.RegisterType<ISecCommandContext, QuintessenceCommandContext>();
                UnityContainer.RegisterType<IInfCommandContext, QuintessenceCommandContext>();
                UnityContainer.RegisterType<ICrmCommandContext, QuintessenceCommandContext>();
                UnityContainer.RegisterType<ISimCommandContext, QuintessenceCommandContext>();
                UnityContainer.RegisterType<IScmCommandContext, QuintessenceCommandContext>();
                UnityContainer.RegisterType<IRepCommandContext, QuintessenceCommandContext>();
                UnityContainer.RegisterType<IWsmCommandContext, QuintessenceCommandContext>();
                UnityContainer.RegisterType<IDomCommandContext, SharePointCommandContext>();

                //Query repositories
                UnityContainer.RegisterType<ICandidateManagementQueryRepository, CandidateManagementQueryRepository>();
                UnityContainer.RegisterType<IDictionaryManagementQueryRepository, DictionaryManagementQueryRepository>();
                UnityContainer.RegisterType<IDictionaryImportQueryRepository, DictionaryImportQueryRepository>();
                UnityContainer.RegisterType<IProjectManagementQueryRepository, ProjectManagementQueryRepository>();
                UnityContainer.RegisterType<ICustomerRelationshipManagementQueryRepository, CustomerRelationshipManagementQueryRepository>();
                UnityContainer.RegisterType<ISecurityManagementQueryRepository, SecurityManagementQueryRepository>();
                UnityContainer.RegisterType<IFinanceManagementQueryRepository, FinanceManagementQueryRepository>();
                UnityContainer.RegisterType<IInfrastructureQueryRepository, InfrastructureQueryRepository>();
                UnityContainer.RegisterType<IProjectManagementQueryRepository, ProjectManagementQueryRepository>();
                UnityContainer.RegisterType<ISimulationManagementQueryRepository, SimulationManagementQueryRepository>();
                UnityContainer.RegisterType<ISupplyChainManagementQueryRepository, SupplyChainManagementQueryRepository>();
                UnityContainer.RegisterType<IDocumentManagementQueryRepository, DocumentManagementQueryRepository>();
                UnityContainer.RegisterType<IWorkspaceManagementQueryRepository, WorkspaceManagementQueryRepository>();
                UnityContainer.RegisterType<IReportManagementQueryRepository, ReportManagementQueryRepository>();
                UnityContainer.RegisterType<IReportServiceQueryRepository, ReportServiceQueryRepository>();

                //Command repositories
                UnityContainer.RegisterType<ICandidateManagementCommandRepository, CandidateManagementCommandRepository>();
                UnityContainer.RegisterType<IInfrastructureManagementCommandRepository, InfrastructureManagementCommandRepository>();
                UnityContainer.RegisterType<IDictionaryManagementCommandRepository, DictionaryManagementCommandRepository>();
                UnityContainer.RegisterType<ISecurityManagementCommandRepository, SecurityManagementCommandRepository>();
                UnityContainer.RegisterType<IProjectManagementCommandRepository, ProjectManagementCommandRepository>();
                UnityContainer.RegisterType<ICustomerRelationshipManagementCommandRepository, CustomerRelationshipManagementCommandRepository>();
                UnityContainer.RegisterType<ISimulationManagementCommandRepository, SimulationManagementCommandRepository>();
                UnityContainer.RegisterType<ISupplyChainManagementCommandRepository, SupplyChainManagementCommandRepository>();
                UnityContainer.RegisterType<IReportManagementCommandRepository, ReportManagementCommandRepository>();
                UnityContainer.RegisterType<IWorkspaceManagementCommandRepository, WorkspaceManagementCommandRepository>();
                UnityContainer.RegisterType<IDocumentManagementCommandRepository, DocumentManagementCommandRepository>();

                //Query services
                UnityContainer.RegisterType<IAuthenticationQueryService, AuthenticationQueryService>();
                UnityContainer.RegisterType<ICandidateManagementQueryService, CandidateManagementQueryService>();
                UnityContainer.RegisterType<IDictionaryManagementQueryService, DictionaryManagementQueryService>();
                UnityContainer.RegisterType<IProjectManagementQueryService, ProjectManagementQueryService>();
                UnityContainer.RegisterType<ICustomerRelationshipManagementQueryService, CustomerRelationshipManagementQueryService>();
                UnityContainer.RegisterType<IFinanceManagementQueryService, FinanceManagementQueryService>();
                UnityContainer.RegisterType<IInfrastructureQueryService, InfrastructureQueryService>();
                UnityContainer.RegisterType<IProjectManagementQueryService, ProjectManagementQueryService>();
                UnityContainer.RegisterType<ISimulationManagementQueryService, SimulationManagementQueryService>();
                UnityContainer.RegisterType<ISupplyChainManagementQueryService, SupplyChainManagementQueryService>();
                UnityContainer.RegisterType<IDocumentManagementQueryService, DocumentManagementQueryService>();
                UnityContainer.RegisterType<IWorkspaceManagementQueryService, WorkspaceManagementQueryService>();
                UnityContainer.RegisterType<IReportManagementQueryService, ReportManagementQueryService>();
                UnityContainer.RegisterType<ISecurityQueryService, SecurityQueryService>();

                //Command services
                UnityContainer.RegisterType<IAuthenticationCommandService, AuthenticationCommandService>();
                UnityContainer.RegisterType<IInfrastructureCommandService, InfrastructureCommandService>();
                UnityContainer.RegisterType<ICandidateManagementCommandService, CandidateManagementCommandService>();
                UnityContainer.RegisterType<IDictionaryManagementCommandService, DictionaryManagementCommandService>();
                UnityContainer.RegisterType<IProjectManagementCommandService, ProjectManagementCommandService>();
                UnityContainer.RegisterType<ICustomerRelationshipManagementCommandService, CustomerRelationshipManagementCommandService>();
                UnityContainer.RegisterType<ISimulationManagementCommandService, SimulationManagementCommandService>();
                UnityContainer.RegisterType<ISupplyChainManagementCommandService, SupplyChainManagementCommandService>();
                UnityContainer.RegisterType<IReportManagementCommandService, ReportManagementCommandService>();
                UnityContainer.RegisterType<IWorkspaceManagementCommandService, WorkspaceManagementCommandService>();
                UnityContainer.RegisterType<IDocumentManagementCommandService, DocumentManagementCommandService>();

                RegisterValidationRules();

                return UnityContainer;
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return null;
            }
        }

        private static void RegisterValidationRules()
        {
            //ValidationRuleEngine.RegisterRule<ProjectCandidateCategoryDetailType1>((container, entity) => 
            //{
            //    return true;
            //});
        }
    }
}
