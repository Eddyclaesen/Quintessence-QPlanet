using System;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Scm;

namespace Quintessence.QPlanet.ViewModel.Scm
{
    public class ActivityProfileSelectListItemModel : SelectListItem
    {
        public ActivityProfileSelectListItemModel(ActivityProfileView activityProfileView)
        {
            Id = activityProfileView.Id;
            Name = string.Format("{0} - {1}", activityProfileView.Activity.Name, activityProfileView.ProfileName);
        }

        public Guid Id
        {
            get { return new Guid(Value); }
            set { Value = value.ToString(); }
        }

        public string Name
        {
            get { return Text; }
            set { Text = value; }
        }
    }
}