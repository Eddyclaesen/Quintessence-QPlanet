using System.Web.Mvc;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class ProjectTypeSelectListItemModel : SelectListItem
    {
        public string Id
        {
            get { return Value; }
            set { Value = value; }
        }

        public string Name
        {
            get { return Text; }
            set { Text = value; }
        }
    }
}
