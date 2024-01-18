using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProductsheetEntryView : ViewEntityBase
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public UserView User { get; set; }

        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public ProjectView Project { get; set; }

        [DataMember]
        public Guid ProjectPlanPhaseId { get; set; }
        
        [DataMember]
        public ProjectPlanPhaseView ProjectPlanPhase { get; set; }

        [DataMember]
        public Guid ProductId { get; set; }

        [DataMember]
        public int Quantity { get; set; }
        
        [DataMember]
        public DateTime Date { get; set; }
        
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
        public string Description { get; set; }

        [DataMember]
        public string BceEntity { get; set; }

        public InvoiceStatusType Status
        {
            get { return (InvoiceStatusType)InvoiceStatusCode; }
            set { InvoiceStatusCode = (int)value; }
        }
    }
}