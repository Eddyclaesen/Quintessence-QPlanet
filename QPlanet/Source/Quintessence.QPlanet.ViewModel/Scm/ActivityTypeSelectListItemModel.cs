using System;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Scm;

namespace Quintessence.QPlanet.ViewModel.Scm
{
    public class ActivityTypeSelectListItemModel : SelectListItem
    {
        public ActivityTypeSelectListItemModel(ActivityTypeView activityType)
        {
            Id = activityType.Id;
            Name = activityType.Name;
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