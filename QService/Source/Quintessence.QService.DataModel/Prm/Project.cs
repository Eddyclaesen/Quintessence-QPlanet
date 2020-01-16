using System;
using System.ComponentModel.DataAnnotations;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class Project : EntityBase
    {
        [Required]
        public string Name { get; set; }

        public Guid ProjectTypeId { get; set; }

        [Required]
        public Guid? ProjectManagerId { get; set; }
        
        [Range(1, 2, ErrorMessage = "Only 'Time & Material' of 'Fixed Price' are allowed as Pricing Model.")]
        public int PricingModelId { get; set; }

        public Guid? CoProjectManagerId { get; set; }

        [Required]
        public Guid? CustomerAssistantId { get; set; }

        public int StatusCode { get; set; }

        public bool Confidential { get; set; }

        public string Remarks { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Illegal customer selected.")]
        public int ContactId { get; set; }

        public string DepartmentInformation { get; set; }

        public override System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(ProjectManagerId.HasValue && ProjectManagerId == Guid.Empty)
                yield return new ValidationResult("Project manager is required.");
        }

        public Guid? ProposalId { get; set; }
    }
}
