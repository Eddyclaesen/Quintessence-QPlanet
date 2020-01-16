using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Rep
{
    [DataContract(IsReference = true)]
    public class ReportParameterValueView : ViewEntityBase
    {
        [DataMember]
        public Guid ReportParameterId { get; set; }

        [DataMember]
        public ReportParameterView ReportParameter { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public string Text { get; set; }
    }
}