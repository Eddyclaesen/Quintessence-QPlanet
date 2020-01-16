using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Fin
{
    [DataContract(IsReference = true)]
    public class InvoicingConsultancyProjectFixedPriceEntryView : InvoicingBaseEntryView
    {
        [DataMember]
        public DateTime Date { get; set; }
    }
}