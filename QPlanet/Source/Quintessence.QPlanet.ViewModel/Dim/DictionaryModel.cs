using System.Collections.Generic;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Dim
{
    public class DictionaryModel : BaseEntityModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool Current { get; set; }

        public List<DictionaryClusterModel> DictionaryClusters { get; set; }
    }
}
