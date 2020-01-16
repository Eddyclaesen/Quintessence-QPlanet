using System;

namespace Quintessence.QJobService.Scheduler.Model
{
    public class JobSchedule
    {
        public Guid Id { get; set; }
        public Guid JobDefinitionId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Interval { get; set; }
        public bool IsEnabled { get; set; }
    }
}