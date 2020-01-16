using System;
using System.ComponentModel.DataAnnotations;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectPlanPhaseActivity : ProjectPlanPhaseEntry
    {
        [Required]
        public Guid ActivityId { get; set; }

        [Required]
        public Guid ProfileId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The duration of the activity must be greater than 0.")]
        public decimal Duration { get; set; }

        public string Notes { get; set; }
    }
}
