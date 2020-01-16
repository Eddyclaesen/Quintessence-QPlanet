using System.Data.Entity;
using Quintessence.QService.Data.Interfaces.CommandContext;
using Quintessence.QService.DataModel.Dim;

namespace Quintessence.QService.Data.CommandContext
{
    /// <summary>
    /// Quintessence data context
    /// </summary>
    public partial class QuintessenceCommandContext : IDimCommandContext
    {
        public IDbSet<Dictionary> Dictionaries { get; set; }
        public IDbSet<DictionaryCluster> DictionaryClusters { get; set; }
        public IDbSet<DictionaryClusterTranslation> DictionaryClusterTranslations { get; set; }
        public IDbSet<DictionaryCompetence> DictionaryCompetences { get; set; }
        public IDbSet<DictionaryCompetenceTranslation> DictionaryCompetenceTranslations { get; set; }
        public IDbSet<DictionaryLevel> DictionaryLevels { get; set; }
        public IDbSet<DictionaryLevelTranslation> DictionaryLevelTranslations { get; set; }
        public IDbSet<DictionaryIndicator> DictionaryIndicators { get; set; }
        public IDbSet<DictionaryIndicatorTranslation> DictionaryIndicatorTranslations { get; set; }
    }
}
