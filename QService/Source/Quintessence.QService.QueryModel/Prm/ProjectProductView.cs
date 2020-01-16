using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectProductView : ViewEntityBase
    {
        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public Guid ProductTypeId { get; set; }

        [DataMember]
        public decimal? InvoiceAmount { get; set; }

        [DataMember]
        public int InvoiceStatusCode { get; set; }

        [DataMember]
        public string InvoiceRemarks { get; set; }

        [DataMember]
        public string PurchaseOrderNumber { get; set; }

        [DataMember]
        public string InvoiceNumber { get; set; }

        [DataMember]
        public DateTime? InvoicedDate { get; set; }

        [DataMember]
        public string ProductTypeName { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public DateTime? Deadline { get; set; }

        [DataMember]
        public bool NoInvoice { get; set; }
    }
}