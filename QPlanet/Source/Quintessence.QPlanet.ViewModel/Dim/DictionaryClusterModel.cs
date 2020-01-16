using System.Collections.Generic;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Dim
{
    public class DictionaryClusterModel : BaseEntityModel
    {
        public string Name { get; set; }

        public int Order { get; set; }

        public List<DictionaryCompetenceModel> DictionaryCompetences { get; set; }
    }
}
