using System.Globalization;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class AdviceSelectListItemModel : CheckboxItemModelBase
    {
        public AdviceSelectListItemModel(AdviceView adviceView, int? selectedAdviceId = null)
        {
            Id = adviceView.Id.ToString(CultureInfo.InvariantCulture);
            Name = adviceView.Name;
            IsChecked = selectedAdviceId != null && selectedAdviceId.Value == adviceView.Id;
        }

        public string Id { get; set; }

        public string Name { get; set; }
    }
}