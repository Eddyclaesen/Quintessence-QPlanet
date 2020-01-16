using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Inf;

namespace Quintessence.QPlanet.ViewModel.Dim
{
    public class EditDictionaryLevelModel : BaseEntityModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Level")]
        public int Level { get; set; }

        [Display(Name = "# of usages")]
        public int DictionaryNumberOfUsages { get; set; }

        [Display(Name = "Is dictionary live")]
        public bool DictionaryIsLive { get; set; }

        public string DictionaryName { get; set; }

        public Guid DictionaryId { get; set; }

        public string DictionaryClusterName { get; set; }

        public Guid DictionaryClusterId { get; set; }

        public string DictionaryCompetenceName { get; set; }

        public Guid DictionaryCompetenceId { get; set; }

        public List<EditDictionaryLevelTranslationModel> DictionaryLevelTranslations { get; set; }

        public List<EditDictionaryIndicatorModel> DictionaryIndicators { get; set; }
    }
}