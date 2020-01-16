using System.Collections.Generic;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Dim
{
    public class DictionaryCompetenceModel : BaseEntityModel
    {
        public string Name { get; set; }

        public List<DictionaryLevelModel> DictionaryLevels { get; set; }

        public int Order { get; set; }
    }
}
