using System;
using System.Collections.Generic;
using Quintessence.QService.DataModel.Sim;

namespace Quintessence.QService.Business.Interfaces.CommandRepositories
{
    public interface ISimulationManagementCommandRepository : ICommandRepository
    {
        List<SimulationCombination2Language> ListSimulationCombinationLanguages(Guid id);
        void UnlinkSimulationCombinationLanguage(Guid simulationId, int languageId);
        void LinkSimulationCombinationLanguage(Guid id, int languageId);
    }
}
