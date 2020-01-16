using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Core.Unity;

namespace Quintessence.QService.QPlanetService.Implementation.Base
{
    public class UnityServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            try
            {
                var host = new UnityServiceHost(ServiceHostFactoryHelper.CreateUnityContainer(), serviceType, baseAddresses);

                return host;
            }
            catch (Exception exception)
            {
                LogManager.LogError(exception);
                return null;
            }
        }
    }
}
