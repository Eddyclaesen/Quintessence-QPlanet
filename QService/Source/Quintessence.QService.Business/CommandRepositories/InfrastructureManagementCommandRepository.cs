using System;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Data.Interfaces.CommandContext;

namespace Quintessence.QService.Business.CommandRepositories
{
    public class InfrastructureManagementCommandRepository : CommandRepositoryBase<IInfCommandContext>,
                                                             IInfrastructureManagementCommandRepository
    {
        public InfrastructureManagementCommandRepository(IUnityContainer container) : base(container)
        {
        }

        public void ScheduleJob(Guid jobDefinitionId)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {
                        var job = context.Jobs.Add(context.Jobs.Create());
                        job.Id = Guid.NewGuid();
                        job.JobDefinitionId = jobDefinitionId;
                        job.RequestDate = DateTime.Now;
                        context.SaveChanges();
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