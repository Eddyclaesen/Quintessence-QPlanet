using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProductSheetEntry : EntityBase, IInvoiceInfo
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid ProjectPlanPhaseId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public int InvoiceStatusCode { get; set; }
        public DateTime? InvoicedDate { get; set; }
        public string InvoiceRemarks { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public string Description { get; set; }

        public Guid? ProposalId { get; set; }
    }
}