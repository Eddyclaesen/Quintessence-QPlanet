using System;
using System.Collections.Generic;
using Quintessence.QService.DataModel.Dim;

namespace Quintessence.QService.Business.Interfaces.CommandRepositories
{
    public interface IDictionaryManagementCommandRepository : ICommandRepository, IDisposable
    {
        void SaveDictionary(Dictionary dictionary);
    }
}
