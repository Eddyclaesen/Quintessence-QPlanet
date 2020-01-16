using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectProduct : EntityBase, IInvoiceInfo
    {
        public Guid ProjectId { get; set; }
        public Guid ProductTypeId { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public int InvoiceStatusCode { get; set; }
        public DateTime? InvoicedDate { get; set; }
        public string InvoiceRemarks { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public bool NoInvoice { get; set; }

        public Guid? ProposalId { get; set; }
    }
}