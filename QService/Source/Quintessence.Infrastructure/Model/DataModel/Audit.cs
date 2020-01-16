using System;

namespace Quintessence.Infrastructure.Model.DataModel
{
    public class Audit
    {
        public string CreatedBy { get; set; }
        
        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string DeletedBy { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }

        public Guid VersionId { get; set; }
    }
}
