using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.Core.Configuration;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.Data.QueryContext;
using Quintessence.QService.SharePointData.QueryContext;

namespace Quintessence.QService.Data.IntegrationTests.Base
{
    [TestClass]
    public abstract class TestBase
    {
        private readonly IUnityContainer _container;

        protected TestBase()
        {
            _container = new UnityContainer();
            _container.RegisterInstance<IConfiguration>(new DataIntegrationTestConfiguration());
            _container.RegisterType<IQuintessenceQueryContext, QuintessenceQueryContext>();
            _container.RegisterType<ICamQueryContext, QuintessenceQueryContext>();
            _container.RegisterType<ICrmQueryContext, QuintessenceQueryContext>();
            _container.RegisterType<IDimQueryContext, QuintessenceQueryContext>();
            _container.RegisterType<IDomQueryContext, SharePointQueryContext>();
            _container.RegisterType<IInfQueryContext, QuintessenceQueryContext>();
            _container.RegisterType<IPrmQueryContext, QuintessenceQueryContext>();
            _container.RegisterType<IRepQueryContext, QuintessenceQueryContext>();
            _container.RegisterType<IScmQueryContext, QuintessenceQueryContext>();
            _container.RegisterType<ISecQueryContext, QuintessenceQueryContext>();
            _container.RegisterType<ISimQueryContext, QuintessenceQueryContext>();
            _container.RegisterType<IWsmQueryContext, QuintessenceQueryContext>();
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