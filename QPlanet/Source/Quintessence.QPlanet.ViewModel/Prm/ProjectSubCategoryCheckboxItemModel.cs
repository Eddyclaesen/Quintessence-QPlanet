using System;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class ProjectSubCategoryCheckboxItemModel : CheckboxItemModelBase
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ProjectStatusCodeViewType Status { get; set; }

        public bool IsReadOnly
        {
            get { return Status != ProjectStatusCodeViewType.Draft && IsChecked; }
        }
    }
}