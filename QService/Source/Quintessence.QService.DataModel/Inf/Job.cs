using System;

namespace Quintessence.QService.DataModel.Inf
{
    public class Job
    {
        public Guid Id { get; set; }
        public Guid JobDefinitionId { get; set; }
        public Guid? JobScheduleId { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? Success { get; set; }
    }
}