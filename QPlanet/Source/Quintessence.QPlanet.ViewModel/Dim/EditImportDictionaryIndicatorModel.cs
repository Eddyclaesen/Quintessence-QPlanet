using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quintessence.QPlanet.ViewModel.Dim
{
    public class EditImportDictionaryIndicatorModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        public int Order { get; set; }

        public List<EditImportDictionaryIndicatorTranslationModel> DictionaryIndicatorTranslations { get; set; }
    }
}