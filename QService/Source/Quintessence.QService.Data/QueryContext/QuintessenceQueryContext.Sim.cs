using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Quintessence.QService.Data.Interfaces.QueryContext;
using Quintessence.QService.QueryModel.Sim;

namespace Quintessence.QService.Data.QueryContext
{
    /// <summary>
    /// Quintessence data context
    /// </summary>
    public partial class QuintessenceQueryContext : ISimQueryContext
    {
        public DbQuery<SimulationSetView> SimulationSets { get { return Set<SimulationSetView>().AsNoTracking(); } }
        public DbQuery<SimulationDepartmentView> SimulationDepartments { get { return Set<SimulationDepartmentView>().AsNoTracking(); } }
        public DbQuery<SimulationLevelView> SimulationLevels { get { return Set<SimulationLevelView>().AsNoTracking(); } }
        public DbQuery<SimulationView> Simulations { get { return Set<SimulationView>().AsNoTracking(); } }
        public DbQuery<SimulationTranslationView> SimulationTranslations { get { return Set<SimulationTranslationView>().AsNoTracking(); } }
        public DbQuery<SimulationMatrixEntryView> SimulationMatrixEntries { get { return Set<SimulationMatrixEntryView>().AsNoTracking(); } }
        public DbQuery<SimulationCombinationView> SimulationCombinations { get { return Set<SimulationCombinationView>().AsNoTracking(); } }
        public DbQuery<SimulationContextView> SimulationContexts { get { return Set<SimulationContextView>().AsNoTracking(); } }
        public DbQuery<SimulationContextUserView> SimulationContextUsers { get { return Set<SimulationContextUserView>().AsNoTracking(); } }

        public IEnumerable<SimulationCombinationLanguageView> ListSimulationCombinationLanguages(Guid id)
        {
            var query = Database.SqlQuery<SimulationCombinationLanguageView>("Simulation_ListSimulationCombinationLanguages {0}", id);
            return query;
        }
    }
}
