using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Quintessence.QService.Core.Validation;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectPlanPhase : EntityBase
    {
        [Required]
        public Guid ProjectPlanId { get; set; }

        [Required(ErrorMessage = "The name of the project plan phase is required.")]
        public string Name { get; set; }

        [DateRange(ErrorMessage = "The start date for the project plan must be between {1} and {2}.")]
        public DateTime StartDate { get; set; }

        [DateRange(ErrorMessage = "The end date for the project plan must be between {1} and {2}.")]
        public DateTime EndDate { get; set; }
        
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate > EndDate)
                yield return new ValidationResult("The start date of the project plan phase is greater than the end date.");
        }
    }
}
