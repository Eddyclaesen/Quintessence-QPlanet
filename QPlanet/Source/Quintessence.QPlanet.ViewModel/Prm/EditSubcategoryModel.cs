using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditSubcategoryModel : BaseEntityModel
    {
        [Required]
        public string Name { get; set; }

        [MaxLength(10)]
        public string Code { get; set; }

        [Display(Name = "TaskId in SO")]
        public int? CrmTaskId { get; set; }

        [Display(Name = "Execution time (min)")]
        public int? Execution { get; set; }

        [Display(Name = "Color (Ex: 0088DD)")]
        public string Color { get; set; }

        [Required]
        [Display(Name="Type")]
        public int SubcategoryType { get; set; }

        public List<EditProjectTypeCategoryDefaultValueModel> ProjectTypeCategoryDefaultValues { get; set; }

        public IEnumerable<SelectListItem> CreateSubcategoryTypesSelectListItems()
        {
            return Enum.GetValues(typeof(SubcategoryType)).OfType<SubcategoryType>().Select(sct =>
                                                                                            new SelectListItem
                                                                                            {
                                                                                                Value = ((int)sct).ToString(CultureInfo.InvariantCulture),
                                                                                                Text = EnumMemberNameAttribute.GetName(sct),
                                                                                                Selected = (int)sct == SubcategoryType
                                                                                            });
        }
    }
}
