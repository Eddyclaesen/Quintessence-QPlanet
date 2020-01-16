using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quintessence.QPlanet.ViewModel.Dim
{
    public class EditImportDictionaryCompetenceModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public int Order { get; set; }

        public List<EditImportDictionaryCompetenceTranslationModel> DictionaryCompetenceTranslations { get; set; }

        public List<EditImportDictionaryLevelModel> DictionaryLevels { get; set; }
    }
}