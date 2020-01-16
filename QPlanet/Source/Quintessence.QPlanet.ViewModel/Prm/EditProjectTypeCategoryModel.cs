using System.Collections.Generic;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectTypeCategoryModel : BaseEntityModel
    {
        private List<EditProjectTypeCategoryLevelModel> _projectTypeCategoryLevels;

        public List<EditProjectTypeCategoryLevelModel> ProjectTypeCategoryLevels
        {
            get { return _projectTypeCategoryLevels ?? (_projectTypeCategoryLevels = new List<EditProjectTypeCategoryLevelModel>()); }
        }

        public string Name { get; set; }
    }
}