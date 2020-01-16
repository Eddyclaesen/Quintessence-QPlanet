using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Inf;

namespace Quintessence.QPlanet.ViewModel.Dim
{
    public class EditDictionaryModel : BaseEntityModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Owner")]
        public string ContactFullName { get; set; }

        public List<EditDictionaryClusterModel> DictionaryClusters { get; set; }

        public List<LanguageView> Languages { get; set; }

        [Display(Name = "# of usages")]
        public int NumberOfUsages { get; set; }

        [Display(Name = "Is live")]
        public bool IsLive { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}