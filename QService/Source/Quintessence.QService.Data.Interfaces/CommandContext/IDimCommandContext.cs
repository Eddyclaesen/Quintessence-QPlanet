using System.Data.Entity;
using Quintessence.QService.DataModel.Dim;

namespace Quintessence.QService.Data.Interfaces.CommandContext
{
    /// <summary>
    /// Interface for the Dictionairy Management data context
    /// </summary>
    public interface IDimCommandContext : IQuintessenceCommandContext
    {
        IDbSet<Dictionary> Dictionaries { get; set; }
        IDbSet<DictionaryCluster> DictionaryClusters { get; set; }
        IDbSet<DictionaryClusterTranslation> DictionaryClusterTranslations { get; set; }
        IDbSet<DictionaryCompetence> DictionaryCompetences { get; set; }
        IDbSet<DictionaryCompetenceTranslation> DictionaryCompetenceTranslations { get; set; }
        IDbSet<DictionaryLevel> DictionaryLevels { get; set; }
        IDbSet<DictionaryLevelTranslation> DictionaryLevelTranslations { get; set; }
        IDbSet<DictionaryIndicator> DictionaryIndicators { get; set; }
        IDbSet<DictionaryIndicatorTranslation> DictionaryIndicatorTranslations { get; set; }
    }
}
