using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Inf;

namespace Quintessence.QPlanet.ViewModel.Dim
{
    public class CreateNewDictionaryIndicatorModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        public Guid DictionaryLevelId { get; set; }

        [Display(Name = "Dictionary")]
        public string DictionaryName { get; set; }

        [Display(Name = "Dictionary cluster")]
        public string DictionaryClusterName { get; set; }

        [Display(Name = "Dictionary competence")]
        public string DictionaryCompetenceName { get; set; }

        [Display(Name = "Dictionary level")]
        public string DictionaryLevelName { get; set; }

        [Display(Name = "Is standard")]
        public bool IsStandard { get; set; }

        [Display(Name = "Is distinctive")]
        public bool IsDistinctive { get; set; }

        public List<LanguageView> Languages { get; set; }

        [Display(Name = "Default language")]
        public int LanguageId { get; set; }

        [Display(Name = "Order")]
        public int Order { get; set; }

        public IEnumerable<SelectListItem> CreateLanguageList()
        {
            return Languages.AsSelectListItems(l => l.Id.ToString(CultureInfo.InvariantCulture), l => l.Name);
        }
    }
}