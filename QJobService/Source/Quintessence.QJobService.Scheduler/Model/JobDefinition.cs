using System;
using System.Collections.Generic;

namespace Quintessence.QJobService.Scheduler.Model
{
    public class JobDefinition
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Assembly { get; set; }
        public string Class { get; set; }
        public bool IsEnabled { get; set; }
    }
}
