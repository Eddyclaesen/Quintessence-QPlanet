using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Scm
{
    public class ActivityType2Profile : EntityBase
    {
        public Guid ActivityTypeId { get; set; }

        public Guid ProfileId { get; set; }

        public decimal DayRate { get; set; }

        public decimal HalfDayRate { get; set; }

        public decimal HourlyRate { get; set; }

        public decimal IsolatedHourlyRate { get; set; }
    }
}