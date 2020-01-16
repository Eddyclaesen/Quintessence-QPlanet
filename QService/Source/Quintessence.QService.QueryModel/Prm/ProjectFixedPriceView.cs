using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectFixedPriceView : ViewEntityBase
    {
        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public int InvoiceStatusCode { get; set; }

        [DataMember]
        public DateTime? InvoicedDate { get; set; }

        [DataMember]
        public string InvoiceRemarks { get; set; }

        [DataMember]
        public string PurchaseOrderNumber { get; set; }

        [DataMember]
        public string InvoiceNumber { get; set; }

        [DataMember]
        public DateTime Deadline { get; set; }
    }
}