using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.Data.Interfaces.CommandContext;

namespace Quintessence.QService.Business.CommandRepositories
{
    public class ReportManagementCommandRepository : CommandRepositoryBase<IRepCommandContext>, IReportManagementCommandRepository
    {
        public ReportManagementCommandRepository(IUnityContainer unityContainer)
            : base(unityContainer)
        {
        }
    }
}
