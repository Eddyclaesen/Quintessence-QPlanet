using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Rep
{
    [DataContract(IsReference = true)]
    public class ReportParameterView : ViewEntityBase
    {
        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string DefaultText { get; set; }

        [DataMember]
        public List<ReportParameterValueView> ReportParameterValues { get; set; }
    }
}