using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Dim
{
    public class EditDictionaryClusterModel : BaseEntityModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Color")]
        public string Color { get; set; }

        [Display(Name = "Order")]
        public int Order { get; set; }

        [Display(Name = "Image name")]
        public string ImageName { get; set; }

        [Display(Name = "# of usages")]
        public int DictionaryNumberOfUsages { get; set; }

        [Display(Name = "Is dictionary live")]
        public bool DictionaryIsLive { get; set; }

        public string DictionaryName { get; set; }

        public Guid DictionaryId { get; set; }

        public List<EditDictionaryClusterTranslationModel> DictionaryClusterTranslations { get; set; }

        public List<EditDictionaryCompetenceModel> DictionaryCompetences { get; set; }
    }
}