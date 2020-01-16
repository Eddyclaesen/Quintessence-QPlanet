using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Quintessence.QService.Core.Validation;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Sim
{
    public class SimulationContextUser : EntityBase
    {
        [Required]
        public Guid SimulationContextId { get; set; }

        [Required(ErrorMessage = "A username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "A password is required.")]
        public string Password { get; set; }

        [Required]
        [DateRange(ErrorMessage = "The valid from date must be between {1} and {2}.")]
        public DateTime ValidFrom { get; set; }

        [Required]
        [DateRange(ErrorMessage = "The valid to must be between {1} and {2}.")]
        public DateTime ValidTo { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ValidFrom > ValidTo)
                yield return new ValidationResult(string.Format("The valid from date of the user {0} is greater than the valid to date.", UserName));
        }
    }
}