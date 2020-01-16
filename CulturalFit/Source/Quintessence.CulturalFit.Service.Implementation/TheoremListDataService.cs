using System.Data.Entity.Infrastructure;
using System.Data.Services;
using System.Data.Services.Common;
using System.ServiceModel;
using Microsoft.Practices.Unity;
using Quintessence.CulturalFit.Data.Interfaces;
using Quintessence.CulturalFit.Infra.Unity;

namespace Quintessence.CulturalFit.Service.Implementation
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class TheoremListDataService : DataService<IQContext>
    {
        protected override IQContext CreateDataSource()
        {
            var context = ((UnityServiceHost) OperationContext.Current.Host).Container.Resolve<IQContext>();
            var objectContext = ((IObjectContextAdapter)context).ObjectContext;
            objectContext.ContextOptions.ProxyCreationEnabled = false;
            return context;
        }

        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
        }
    }
}
