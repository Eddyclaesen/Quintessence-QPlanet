using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Dim
{
    public class EditDictionaryIndicatorModel : BaseEntityModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Is standard")]
        public bool IsStandard { get; set; }

        [Display(Name = "Is distinctive")]
        public bool IsDistinctive { get; set; }

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

        public string DictionaryLevelName { get; set; }

        public int DictionaryLevelLevel { get; set; }

        public Guid DictionaryLevelId { get; set; }

        public int Order { get; set; }

        public List<EditDictionaryIndicatorTranslationModel> DictionaryIndicatorTranslations { get; set; }
    }
}