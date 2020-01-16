using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Inf;

namespace Quintessence.QPlanet.ViewModel.Dim
{
    public class CreateNewDictionaryLevelModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        public Guid DictionaryCompetenceId { get; set; }

        [Display(Name = "Dictionary")]
        public string DictionaryName { get; set; }

        [Display(Name = "Dictionary cluster")]
        public string DictionaryClusterName { get; set; }

        [Display(Name = "Dictionary competence")]
        public string DictionaryCompetenceName { get; set; }

        public List<LanguageView> Languages { get; set; }

        [Display(Name = "Default language")]
        public int LanguageId { get; set; }

        [Display(Name = "Level")]
        public int Level { get; set; }

        [Display(Name = "Apply to all competences")]
        public bool ApplyToAllCompetences { get; set; }

        public IEnumerable<SelectListItem> CreateLanguageList()
        {
            return Languages.AsSelectListItems(l => l.Id.ToString(CultureInfo.InvariantCulture), l => l.Name);
        }
    }
}