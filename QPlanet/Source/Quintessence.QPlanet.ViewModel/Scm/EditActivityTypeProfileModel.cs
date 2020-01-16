using System;
using System.Collections.Generic;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Scm
{
    public class EditActivityTypeProfileModel : BaseEntityModel
    {
        public Guid ActivityTypeId { get; set; }

        public IEnumerable<ProfileSelectListItemModel> Profiles { get; set; }

        public Guid ProfileId { get; set; }

        public decimal DayRate { get; set; }

        public decimal HalfDayRate { get; set; }

        public decimal HourlyRate { get; set; }

        public decimal IsolatedHourlyRate { get; set; }
    }
}