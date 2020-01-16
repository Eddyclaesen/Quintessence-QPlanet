using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectInvoiceAmountOverviewEntryView
    {
        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public decimal? InvoiceAmount { get; set; }

        [DataMember]
        public DateTime InvoicedDate { get; set; }

        public int FiscalYear
        {
            get { return InvoicedDate >= new DateTime(InvoicedDate.Year, 4, 1) ? InvoicedDate.Year : InvoicedDate.Year - 1; }
        }
    }
}