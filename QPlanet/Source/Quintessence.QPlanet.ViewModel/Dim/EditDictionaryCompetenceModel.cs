using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Inf;

namespace Quintessence.QPlanet.ViewModel.Dim
{
    public class EditDictionaryCompetenceModel : BaseEntityModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "# of usages")]
        public int DictionaryNumberOfUsages { get; set; }

        [Display(Name = "Is dictionary live")]
        public bool DictionaryIsLive { get; set; }

        public string DictionaryName { get; set; }

        public Guid DictionaryId { get; set; }

        public string DictionaryClusterName { get; set; }

        public Guid DictionaryClusterId { get; set; }

        public int Order { get; set; }

        public List<EditDictionaryCompetenceTranslationModel> DictionaryCompetenceTranslations { get; set; }

        public List<EditDictionaryLevelModel> DictionaryLevels { get; set; }
    }
}