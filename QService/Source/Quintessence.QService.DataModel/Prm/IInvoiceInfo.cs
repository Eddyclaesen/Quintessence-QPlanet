using System;

namespace Quintessence.QService.DataModel.Prm
{
    public interface IInvoiceInfo
    {
        int InvoiceStatusCode { get; set; }
        DateTime? InvoicedDate { get; set; }
    }
}