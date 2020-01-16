using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Inf;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectGeneral
{
    public class CreateEvaluationFormActionModel
    {
        public List<LanguageView> Languages { get; set; }
        public List<EvaluationFormTypeView> EvaluationFormTypes { get; set; }

        public int CrmProjectId { get; set; }

        [Required]
        [Display(Name="First name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Language")]
        public int LanguageId { get; set; }
        [Required]
        [Display(Name = "Evaluation form")]
        public int EvaluationFormTypeId { get; set; }

        public List<SelectListItem> CreateLanguageSelectListItems(int selectedLanguageId)
        {
            return Languages.Select(l => new SelectListItem { Selected = l.Id == selectedLanguageId, Text = l.Name, Value = l.Id.ToString() }).ToList();
        }

        public List<SelectListItem> CreateEvaluationFormSelectListItems(int selectedEvaluationFormId)
        {
            return EvaluationFormTypes.Select(eft => new SelectListItem { Selected = eft.Id == selectedEvaluationFormId, Text = eft.Name, Value = eft.Id.ToString() }).ToList();
        }

        public List<SelectListItem> CreateGenderSelectListItems()
        {
            return Enum.GetValues(typeof(GenderType)).OfType<GenderType>().Select(g =>
                                                                                            new SelectListItem
                                                                                            {
                                                                                                Value = g.ToString(),
                                                                                                Text = EnumMemberNameAttribute.GetName(g),
                                                                                                Selected = g.ToString() == Gender
                                                                                            }).ToList();
        }
    }
}