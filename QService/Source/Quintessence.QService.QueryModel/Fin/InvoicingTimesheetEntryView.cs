using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Fin
{
    [DataContract(IsReference = true)]
    public class InvoicingTimesheetEntryView : InvoicingBaseEntryView
    {
        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}