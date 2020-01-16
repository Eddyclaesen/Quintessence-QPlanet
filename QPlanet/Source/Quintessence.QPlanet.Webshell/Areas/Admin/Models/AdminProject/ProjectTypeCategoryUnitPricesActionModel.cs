using System.Collections.Generic;
using Quintessence.QPlanet.ViewModel.Prm;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Admin.Models.AdminProject
{
    public class ProjectTypeCategoryUnitPricesActionModel
    {
        public List<EditProjectTypeCategoryModel> ProjectTypeMainCategories { get; set; }
        public List<EditProjectTypeCategoryModel> ProjectTypeSubCategories { get; set; }
        public List<ProjectTypeCategoryLevelView> ProjectTypeCategoryLevels { get; set; }
    }
}