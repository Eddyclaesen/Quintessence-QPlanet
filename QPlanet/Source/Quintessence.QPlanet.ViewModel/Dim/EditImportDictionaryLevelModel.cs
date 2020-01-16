using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quintessence.QPlanet.ViewModel.Dim
{
    public class EditImportDictionaryLevelModel
    {
        [Display(Name = "Level")]
        public int Level { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        public List<EditImportDictionaryLevelTranslationModel> DictionaryLevelTranslations { get; set; }

        public List<EditImportDictionaryIndicatorModel> DictionaryIndicators { get; set; }
    }
}