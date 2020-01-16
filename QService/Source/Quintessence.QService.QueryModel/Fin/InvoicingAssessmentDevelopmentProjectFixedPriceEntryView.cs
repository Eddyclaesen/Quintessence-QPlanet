using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Fin
{
    [DataContract(IsReference = true)]
    public class InvoicingAssessmentDevelopmentProjectFixedPriceEntryView : InvoicingBaseEntryView
    {
        [DataMember]
        public DateTime Date { get; set; }
    }
}