using System;
using System.Web.Mvc;

namespace Quintessence.QPlanet.ViewModel.Dim
{
    public class DictionarySelectListItemModel : SelectListItem
    {
        public Guid? Id
        {
            get { return Value != null ? new Guid(Value) : (Guid?)null; }
            set { Value = value != null ? value.ToString() : null; }
        }

        public string Name
        {
            get { return Text; }
            set { Text = value; }
        }
    }
}
