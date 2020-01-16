using Microsoft.Practices.Unity;
using Quintessence.CulturalFit.Business;
using Quintessence.CulturalFit.Business.Interfaces;
using Quintessence.CulturalFit.Data;
using Quintessence.CulturalFit.Data.Interfaces;
using Quintessence.CulturalFit.Infra.Configuration;

namespace Quintessence.CulturalFit.Service.Implementation.Base
{
    public static class ServiceHostFactoryHelper
    {
        public static IUnityContainer CreateUnityContainer()
        {
            var unityContainer = new UnityContainer();

            unityContainer.RegisterInstance<IUnityContainer>(unityContainer);

            unityContainer.RegisterInstance<IConfiguration>(new Configuration());

            unityContainer.RegisterType<IQContext, QContext>();
            unityContainer.RegisterType<ITheoremListRepository, TheoremListRepository>();
            //unityContainer.RegisterType<IAdminRepository, AdminRepository>();
            unityContainer.RegisterType<ICrmRepository, CrmRepository>();

            return unityContainer;
        }
    }
}
