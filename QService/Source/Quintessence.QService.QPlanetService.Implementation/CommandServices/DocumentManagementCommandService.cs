using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.QPlanetService.Contracts.ServiceContracts.CommandServiceContracts;
using Quintessence.QService.QPlanetService.Implementation.Base;

namespace Quintessence.QService.QPlanetService.Implementation.CommandServices
{
    public class DocumentManagementCommandService : SecuredUnityServiceBase, IDocumentManagementCommandService
    {
        public int CreateNewTrainingChecklist()
        {
            LogTrace();

            return ExecuteTransaction(() =>
            {
                var repository = Container.Resolve<IDocumentManagementCommandRepository>();

                var id = repository.PrepareNewTrainingChecklist();

                return id;
            });
        }
    }
}