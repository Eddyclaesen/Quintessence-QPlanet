using System;
using Microsoft.Practices.Unity;
using Quintessence.QService.Business.Interfaces.CommandRepositories;
using Quintessence.QService.Core.Logging;
using Quintessence.QService.Data.Interfaces.CommandContext;
using Quintessence.QService.DataModel.Dim;

namespace Quintessence.QService.Business.CommandRepositories
{
    public class DictionaryManagementCommandRepository : CommandRepositoryBase<IDimCommandContext>, IDictionaryManagementCommandRepository
    {
        public DictionaryManagementCommandRepository(IUnityContainer unityContainer)
            : base(unityContainer)
        {
        }

        public void SaveDictionary(Dictionary dictionary)
        {
            using (DurationLog.Create())
            {
                try
                {
                    using (var context = CreateContext())
                    {

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
