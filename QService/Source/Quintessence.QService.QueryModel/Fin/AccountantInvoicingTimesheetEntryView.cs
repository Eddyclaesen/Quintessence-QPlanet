using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Fin
{
    [DataContract(IsReference = true)]
    public class AccountantInvoicingTimesheetEntryView : AccountantInvoicingBaseEntryView
    {
        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}