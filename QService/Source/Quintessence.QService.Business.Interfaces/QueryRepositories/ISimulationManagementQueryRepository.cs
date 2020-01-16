using System;
using System.Collections.Generic;
using Quintessence.QService.Core.Performance;
using Quintessence.QService.QueryModel.Sim;

namespace Quintessence.QService.Business.Interfaces.QueryRepositories
{
    public interface ISimulationManagementQueryRepository : IQueryRepository
    {
        List<SimulationSetView> ListSimulationSets(PagingInfo pagingInfo = null);
        List<SimulationDepartmentView> ListSimulationDepartments(PagingInfo pagingInfo = null);
        List<SimulationLevelView> ListSimulationLevels(PagingInfo pagingInfo = null);
        List<SimulationView> ListSimulations(PagingInfo pagingInfo = null);
        List<SimulationMatrixEntryView> ListSimulationMatrixEntries(PagingInfo pagingInfo = null);
        List<SimulationCombinationLanguageView> ListSimulationCombinationLanguages(Guid id);
        SimulationView RetrieveSimulation(Guid id);
        SimulationCombinationView RetrieveSimulationCombination(Guid id);
        List<SimulationContextView> ListSimulationContexts();
        List<SimulationContextUserView> ListSimulationContextUsers(Guid simulationContextId);
        List<SimulationTranslationView> ListSimulationTranslations(Guid simulationId);
    }
}