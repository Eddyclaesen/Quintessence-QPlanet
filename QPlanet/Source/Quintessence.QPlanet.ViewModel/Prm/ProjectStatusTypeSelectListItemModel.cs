using System.Globalization;
using System.Web.Mvc;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class ProjectStatusTypeSelectListItemModel : SelectListItem
    {
        public int Id
        {
            get { return int.Parse(Value); }
            set { Value = value.ToString(CultureInfo.InvariantCulture); }
        }

        public string Name
        {
            get { return Text; }
            set { Text = value; }
        }
    }
}