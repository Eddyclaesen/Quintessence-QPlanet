using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quintessence.QPlanet.ViewModel.Dim
{
    public class EditImportDictionaryModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Contact")]
        public int ContactId { get; set; }

        public List<EditImportDictionaryClusterModel> DictionaryClusters { get; set; }
    }
}