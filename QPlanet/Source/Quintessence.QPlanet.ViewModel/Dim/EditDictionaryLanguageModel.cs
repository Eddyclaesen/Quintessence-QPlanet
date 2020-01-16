using System.Collections.Generic;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Inf;

namespace Quintessence.QPlanet.ViewModel.Dim
{
    public class EditDictionaryLanguageModel : BaseEntityModel
    {
        public string Name { get; set; }
        public string ContactFullName { get; set; }
        public List<EditDictionaryClusterModel> DictionaryClusters { get; set; }
        public List<LanguageView> Languages { get; set; }
        public int SelectedLanguageId { get; set; }
    }
}