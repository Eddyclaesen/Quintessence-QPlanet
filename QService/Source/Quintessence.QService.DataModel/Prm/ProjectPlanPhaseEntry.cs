using System;
using System.ComponentModel.DataAnnotations;
using Quintessence.QService.Core.Validation;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectPlanPhaseEntry : EntityBase
    {
        [Required]
        public Guid ProjectPlanPhaseId { get; set; }

        public decimal Quantity { get; set; }

        [DateRange(ErrorMessage = "The deadline must be between {1} and {2}.")]
        public DateTime Deadline { get; set; }
    }
}
