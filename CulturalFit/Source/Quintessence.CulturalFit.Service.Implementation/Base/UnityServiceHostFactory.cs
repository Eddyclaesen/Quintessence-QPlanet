using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Quintessence.CulturalFit.Infra.Unity;

namespace Quintessence.CulturalFit.Service.Implementation.Base
{
    public class UnityServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var host = new UnityServiceHost(ServiceHostFactoryHelper.CreateUnityContainer(), serviceType, baseAddresses);

            return host;
        }
    }
}
