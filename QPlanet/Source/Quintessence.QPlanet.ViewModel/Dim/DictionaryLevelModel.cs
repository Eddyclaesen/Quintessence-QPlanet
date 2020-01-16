using System.Collections.Generic;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Dim
{
    public class DictionaryLevelModel : BaseEntityModel
    {
        public bool IsChecked { get; set; }

        public string Name { get; set; }

        public int Level { get; set; }

        public List<DictionaryIndicatorModel> DictionaryIndicators { get; set; }
    }
}
