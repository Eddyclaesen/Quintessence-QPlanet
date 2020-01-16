using System;
using System.Linq;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Base;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Data.Interfaces.CommandContext;

namespace Quintessence.QService.Business.CommandRepositories
{
    public class DocumentManagementCommandRepository : RepositoryBase<IDomCommandContext>, IDocumentManagementCommandRepository
    {
        public DocumentManagementCommandRepository(IUnityContainer unityContainer)
            : base(unityContainer)
        {
        }

        public int PrepareNewTrainingChecklist()
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = Container.Resolve<IDomCommandContext>())
                    {
                        var id = context.PrepareTrainingChecklist();

                        return id;
                    }
                }
                catch (Exception exception)
                {
                    LogManager.LogError(exception);
                    throw;
                }
            }
        }
    }
}
