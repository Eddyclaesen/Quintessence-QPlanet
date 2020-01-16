using System;

namespace Quintessence.QCare.ViewModel.Base
{
    public abstract class BaseEntityModel
    {
        public Guid Id { get; set; }

        public string AuditCreatedBy { get; set; }

        public DateTime AuditCreatedOn { get; set; }

        public string AuditModifiedBy { get; set; }

        public DateTime? AuditModifiedOn { get; set; }

        public string AuditDeletedBy { get; set; }

        public DateTime? AuditDeletedOn { get; set; }

        public bool AuditIsDeleted { get; set; }

        public Guid AuditVersionId { get; set; }
    }
}
