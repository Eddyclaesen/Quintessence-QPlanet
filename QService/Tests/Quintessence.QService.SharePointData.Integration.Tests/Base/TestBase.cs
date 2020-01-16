using System;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.Core.Configuration;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.SharePointData.QueryContext;

namespace Quintessence.QService.SharePointData.Integration.Tests.Base
{
    [TestClass]
    public abstract class TestBase
    {
        private readonly IUnityContainer _container;

        protected TestBase()
        {
            _container = new UnityContainer();
            _container.RegisterInstance<IConfiguration>(new SharePointDataIntegrationTestConfiguration());
            _container.RegisterType<IDomQueryContext, SharePointQueryContext>();
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