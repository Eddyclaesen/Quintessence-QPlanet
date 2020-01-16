using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quintessence.Infrastructure.Model.DataModel
{
    public abstract class EntityBase : IEntity
    {
        public Guid Id { get; set; }

        public Audit Audit { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Audit == null)
                yield return new ValidationResult("Missing audit information.");

            if (Id == Guid.Empty)
                yield return new ValidationResult(string.Format("Illegal object identifier {0}", Id));
        }
    }
}