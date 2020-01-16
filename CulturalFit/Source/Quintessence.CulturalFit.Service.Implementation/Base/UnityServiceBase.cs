using System.ServiceModel;
using Microsoft.Practices.Unity;
using Quintessence.CulturalFit.Infra.Unity;

namespace Quintessence.CulturalFit.Service.Implementation.Base
{
    public abstract class UnityServiceBase
    {
        private IUnityContainer _container;

        public IUnityContainer Container
        {
            protected get
            {
                return OperationContext.Current == null 
                    ? _container 
                    : ((UnityServiceHost) OperationContext.Current.Host).Container;
            }
            set { _container = value; }
        }
    }
}
