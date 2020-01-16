using Microsoft.Practices.Unity;
using Quintessence.CulturalFit.Business.Interfaces;
using Quintessence.CulturalFit.Data;
using Quintessence.CulturalFit.Data.Interfaces;
using Quintessence.CulturalFit.Infra.Configuration;

namespace Quintessence.CulturalFit.Business.Tests.Base
{
    public abstract class BaseBusiness
    {
        private readonly IUnityContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseBusiness"/> class.
        /// </summary>
        protected BaseBusiness()
        {
            _container = new UnityContainer();
            _container.RegisterInstance<IUnityContainer>(_container);
            _container.RegisterInstance<IConfiguration>(new BusinessTestConfiguration());
            _container.RegisterType<IQContext, QContext>();
            _container.RegisterType<ITheoremListRepository, TheoremListRepository>();
            _container.RegisterType<IAdminRepository, AdminRepository>();
            _container.RegisterType<ICrmRepository, CrmRepository>();
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        public IUnityContainer Container { get { return _container; } }
    }
}
