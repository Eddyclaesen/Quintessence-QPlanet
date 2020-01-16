using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Fin
{
    [DataContract(IsReference = true)]
    public class AccountantInvoicingConsultancyProjectFixedPriceEntryView : AccountantInvoicingBaseEntryView
    {
        [DataMember]
        public DateTime Date { get; set; }
    }
}