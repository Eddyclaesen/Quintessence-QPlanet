using System;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.Infrastructure.Validation;
using Quintessence.QService.Core.Security;
using Quintessence.QService.Core.Testing;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.QueryServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base;
using Quintessence.QService.QPlanetService.Implementation.CommandServices;
using Quintessence.QService.QPlanetService.Implementation.QueryServices;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QService.Business.Integration.Tests.Base
{
    [TestClass]
    public abstract class RepositoryTestBase
    {
        private IUnityContainer _container;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            _container = StaticUnityContainer.UnityContainer = ServiceHostFactoryHelper.CreateUnityContainer();

            _container.RegisterInstance(new ValidationContainer());

            _container.RegisterInstance<IAuthenticationCommandService>(new AuthenticationCommandService());
            _container.RegisterInstance<IAuthenticationQueryService>(new AuthenticationQueryService());

            var authenticationCommandService = _container.Resolve<IAuthenticationCommandService>();
            var response = authenticationCommandService.NegociateAuthenticationToken("admin", "$Quint123");
            var tokenId = StaticAuthenticationToken.TokenId = response.AuthenticationTokenId;

            var authenticationService = _container.Resolve<IAuthenticationQueryService>();
            var token = authenticationService.RetrieveAuthenticationTokenDetail(tokenId);

            Container.RegisterInstance(token);
            Container.RegisterInstance(ConvertToSecurityContext(token));

            _container.RegisterInstance<ICandidateManagementQueryService>(new CandidateManagementQueryService());
            _container.RegisterInstance<ICustomerRelationshipManagementQueryService>(new CustomerRelationshipManagementQueryService());
            _container.RegisterInstance<IDictionaryManagementQueryService>(new DictionaryManagementQueryService());
            _container.RegisterInstance<IDocumentManagementQueryService>(new DocumentManagementQueryService());
            _container.RegisterInstance<IInfrastructureQueryService>(new InfrastructureQueryService());
            _container.RegisterInstance<IProjectManagementQueryService>(new ProjectManagementQueryService());
            _container.RegisterInstance<IReportManagementQueryService>(new ReportManagementQueryService());
            _container.RegisterInstance<ISecurityQueryService>(new SecurityQueryService());
            _container.RegisterInstance<ISimulationManagementQueryService>(new SimulationManagementQueryService());
            _container.RegisterInstance<ISupplyChainManagementQueryService>(new SupplyChainManagementQueryService());
            _container.RegisterInstance<IWorkspaceManagementQueryService>(new WorkspaceManagementQueryService());

            _container.RegisterInstance<ICandidateManagementCommandService>(new CandidateManagementCommandService());
            _container.RegisterInstance<ICustomerRelationshipManagementCommandService>(new CustomerRelationshipManagementCommandService());
            _container.RegisterInstance<IDictionaryManagementCommandService>(new DictionaryManagementCommandService());
            _container.RegisterInstance<IProjectManagementCommandService>(new ProjectManagementCommandService());
            _container.RegisterInstance<IReportManagementCommandService>(new ReportManagementCommandService());
            _container.RegisterInstance<ISecurityCommandService>(new SecurityCommandService());
            _container.RegisterInstance<ISimulationManagementCommandService>(new SimulationManagementCommandService());
            _container.RegisterInstance<ISupplyChainManagementCommandService>(new SupplyChainManagementCommandService());
        }

        private SecurityContext ConvertToSecurityContext(AuthenticationTokenView token)
        {
            return new SecurityContext
            {
                TokenId = token.Id,
                UserId = token.UserId,
                UserName = @"QPlanet\" + token.User.UserName,
            };
        }

        protected IUnityContainer Container
        {
            get { return _container; }
        }

        public TContextInterface CreateContext<TContextInterface>()
            where TContextInterface : IDisposable
        {
            var context = Container.Resolve<TContextInterface>();
            Assert.IsNotNull(context);
            return context;
        }
    }
}
