using System.Data.Entity;
using Quintessence.QService.DataModel.Sim;

namespace Quintessence.QService.Data.Interfaces.CommandContext
{
    public interface ISimCommandContext : IQuintessenceCommandContext
    {
        IDbSet<SimulationSet> SimulationSets { get; set; }
        IDbSet<SimulationDepartment> SimulationDepartments { get; set; }
        IDbSet<SimulationLevel> SimulationLevels { get; set; }
        IDbSet<Simulation> Simulations { get; set; }
        IDbSet<SimulationTranslation> SimulationTranslations { get; set; }
        IDbSet<SimulationCombination> SimulationCombinations { get; set; }
        IDbSet<SimulationContext> SimulationContexts { get; set; }
        IDbSet<SimulationContextUser> SimulationContextUsers { get; set; }
        IDbSet<SimulationCombination2Language> SimulationCombinationLanguages { get; set; }
    }
}