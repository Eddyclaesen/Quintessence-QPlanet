using System;
using System.ComponentModel.DataAnnotations;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectFixedPrice : EntityBase, IInvoiceInfo
    {
        public Guid ProjectId { get; set; }
        public decimal Amount { get; set; }
        public int InvoiceStatusCode { get; set; }
        public DateTime? InvoicedDate { get; set; }
        public string InvoiceRemarks { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime Deadline { get; set; }
        public string BceEntity { get; set; }
        public Guid? ProposalId { get; set; }

        public override System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Amount == default(decimal))
                yield return new ValidationResult("Please enter an amount that is greater than zero.");
        }
    }
}