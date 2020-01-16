using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quintessence.QPlanet.ViewModel.Dim
{
    public class EditImportDictionaryClusterModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
        
        public int Order { get; set; }

        public List<EditImportDictionaryClusterTranslationModel> DictionaryClusterTranslations { get; set; }

        public List<EditImportDictionaryCompetenceModel> DictionaryCompetences { get; set; }
    }
}