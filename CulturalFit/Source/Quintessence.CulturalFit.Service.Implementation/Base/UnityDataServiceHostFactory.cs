using System.Data.Services;
using Quintessence.CulturalFit.Infra.Unity;

namespace Quintessence.CulturalFit.Service.Implementation.Base
{
    public class UnityDataServiceHostFactory : DataServiceHostFactory
    {
        protected override System.ServiceModel.ServiceHost CreateServiceHost(System.Type serviceType, System.Uri[] baseAddresses)
        {
            var host = new UnityServiceHost(ServiceHostFactoryHelper.CreateUnityContainer(), serviceType, baseAddresses);

            return host;
        }
    }
}
