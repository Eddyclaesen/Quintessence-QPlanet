using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectPlanPhaseProductView : ProjectPlanPhaseEntryView
    {
        [DataMember]
        public Guid ProductId { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public Guid ProductTypeId { get; set; }

        [DataMember]
        public string ProductTypeName { get; set; }

        [DataMember]
        public decimal UnitPrice { get; set; }

        [DataMember]
        public decimal TotalPrice { get; set; }

        [DataMember]
        public bool NoInvoice { get; set; }

        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public Guid? ProductsheetEntryId { get; set; }
    }
}