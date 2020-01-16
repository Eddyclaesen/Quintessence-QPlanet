using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quintessence.QPlanet.ViewModel.Scm
{
    public class AddActivityTypeProfileModel
    {
        public Guid ActivityTypeId { get; set; }

        public IEnumerable<ProfileSelectListItemModel> Profiles { get; set; }

        public Guid ProfileId { get; set; }

        [Range(0d, double.MaxValue)]
        public decimal DayRate { get; set; }

        [Range(0d, double.MaxValue)]
        public decimal HalfDayRate { get; set; }

        [Range(0d, double.MaxValue)]
        public decimal HourlyRate { get; set; }
        
        [Range(0d, double.MaxValue)]
        public decimal IsolatedHourlyRate { get; set; }
    }
}