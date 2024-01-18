using System;
using System.Web.Mvc;

namespace Quintessence.QPlanet.ViewModel.Dim
{
    public class BceSelectListItemModel : SelectListItem
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
