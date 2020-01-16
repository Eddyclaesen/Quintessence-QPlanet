using System;

namespace Quintessence.QService.Business.Interfaces.CommandRepositories
{
    public interface IInfrastructureManagementCommandRepository : ICommandRepository
    {
        void ScheduleJob(Guid jobDefinitionId);
    }
}