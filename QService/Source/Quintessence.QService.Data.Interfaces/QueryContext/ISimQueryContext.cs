using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Quintessence.QService.QueryModel.Sim;

namespace Quintessence.QService.Data.Interfaces.QueryContext
{
    public interface ISimQueryContext : IQuintessenceQueryContext
    {
        DbQuery<SimulationSetView> SimulationSets { get; }
        DbQuery<SimulationDepartmentView> SimulationDepartments { get; }
        DbQuery<SimulationLevelView> SimulationLevels { get; }
        DbQuery<SimulationView> Simulations { get; }
        DbQuery<SimulationTranslationView> SimulationTranslations { get; }
        DbQuery<SimulationMatrixEntryView> SimulationMatrixEntries { get; }
        DbQuery<SimulationCombinationView> SimulationCombinations { get; }
        DbQuery<SimulationContextView> SimulationContexts { get; }
        DbQuery<SimulationContextUserView> SimulationContextUsers { get; }
        IEnumerable<SimulationCombinationLanguageView> ListSimulationCombinationLanguages(Guid id);
    }
}