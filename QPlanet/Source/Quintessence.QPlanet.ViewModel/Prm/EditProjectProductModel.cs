using System;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectProductModel : BaseEntityModel
    {
        public decimal? InvoiceAmount { get; set; }
        public int InvoiceStatusCode { get; set; }
        public InvoiceStatusType InvoiceStatus { get { return (InvoiceStatusType) InvoiceStatusCode; } }
        public string InvoiceStatusName { get; set; }
        public DateTime? InvoicedDate { get; set; }
        public string ProductTypeName { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public bool NoInvoice { get; set; }
        public string InvoiceRemarks { get; set; }
        public string BceEntity { get; set; }
    }
}