using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectSubCategoryDetailModelBase : BaseEntityModel
    {
        public string Name { get; set; }

        [Required]
        [AllowHtml]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Survey moment")]
        public int SurveyPlanningId { get; set; }

        public IEnumerable<SelectListItem> CreateSurveyPlanningSelectListItems()
        {
            return Enum.GetValues(typeof (SurveyPlanningType)).OfType<SurveyPlanningType>().Select(spt =>
                                                                                            new SelectListItem
                                                                                                {
                                                                                                    Value = ((int)spt).ToString(),
                                                                                                    Text = EnumMemberNameAttribute.GetName(spt),
                                                                                                    Selected = (int)spt == SurveyPlanningId
                                                                                                });
        }
    }
}
