using System.Data.Entity;
using Quintessence.QService.Data.Interfaces.CommandContext;
using Quintessence.QService.DataModel.Sim;

namespace Quintessence.QService.Data.CommandContext
{
    /// <summary>
    /// Quintessence data context
    /// </summary>
    public partial class QuintessenceCommandContext : ISimCommandContext
    {
        public IDbSet<SimulationSet> SimulationSets { get; set; }
        public IDbSet<SimulationDepartment> SimulationDepartments { get; set; }
        public IDbSet<SimulationLevel> SimulationLevels { get; set; }
        public IDbSet<Simulation> Simulations { get; set; }
        public IDbSet<SimulationTranslation> SimulationTranslations { get; set; }
        public IDbSet<SimulationCombination> SimulationCombinations { get; set; }
        public IDbSet<SimulationContext> SimulationContexts { get; set; }
        public IDbSet<SimulationContextUser> SimulationContextUsers { get; set; }
        public IDbSet<SimulationCombination2Language> SimulationCombinationLanguages { get; set; }
    }
}
