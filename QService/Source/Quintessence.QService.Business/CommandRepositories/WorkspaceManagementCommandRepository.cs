using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.Data.Interfaces.CommandContext;

namespace Quintessence.QService.Business.CommandRepositories
{
    public class WorkspaceManagementCommandRepository : CommandRepositoryBase<IWsmCommandContext>, IWorkspaceManagementCommandRepository
    {
        public WorkspaceManagementCommandRepository(IUnityContainer container)
            : base(container)
        {
        }
    }
}