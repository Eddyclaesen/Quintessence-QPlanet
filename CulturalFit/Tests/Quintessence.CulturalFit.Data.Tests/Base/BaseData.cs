using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.CulturalFit.Data.Interfaces;
using Quintessence.CulturalFit.Infra.Configuration;

namespace Quintessence.CulturalFit.Data.Tests.Base
{
    [TestClass]
    public abstract class BaseData
    {
        private readonly IUnityContainer _container;

        protected BaseData()
        {
            _container = new UnityContainer();
            _container.RegisterInstance<IUnityContainer>(_container);
            _container.RegisterInstance<IConfiguration>(new DataTestConfiguration());
            _container.RegisterType<IQContext, QDataTestContext>();
        }

        protected IUnityContainer Container
        {
            get { return _container; }
        }

        public IQContext CreateContext()
        {
            var context = Container.Resolve<IQContext>();
            Assert.IsNotNull(context);
            return context;
        }
    }
}