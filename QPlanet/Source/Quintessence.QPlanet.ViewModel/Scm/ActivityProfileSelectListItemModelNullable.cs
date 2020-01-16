using System;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Scm;

namespace Quintessence.QPlanet.ViewModel.Scm
{
    public class ActivityProfileSelectListItemModelNullable : SelectListItem
    {
        public ActivityProfileSelectListItemModelNullable(ActivityProfileView activityProfileView)
        {
            if (activityProfileView != null)
            {
                Id = activityProfileView.Id;
                Name = string.Format("{0} - {1}", activityProfileView.Activity.Name, activityProfileView.ProfileName);
            }
        }

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