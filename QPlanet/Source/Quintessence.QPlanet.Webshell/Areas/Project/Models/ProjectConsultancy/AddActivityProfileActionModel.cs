using System;
using System.Collections.Generic;
using Quintessence.QPlanet.ViewModel.Scm;
using Quintessence.QService.QueryModel.Scm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectConsultancy
{
    public class AddActivityProfileActionModel
    {
        public List<ActivityTypeProfileView> Profiles;

        public List<ActivityTypeProfileSelectListItemModel> ProfileSelectListItems { get; set; }

        public Guid ProfileId { get; set; }

        public Guid ActivityId { get; set; }

        public decimal DayRate { get; set; }

        public decimal HalfDayRate { get; set; }

        public decimal HourlyRate { get; set; }

        public decimal IsolatedHourlyRate { get; set; }
    }
}