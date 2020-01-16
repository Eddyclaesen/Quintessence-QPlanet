using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement
{
    [DataContract]
    public class CreateNewReportParameterRequest
    {
        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string DefaultText { get; set; }

        [DataMember]
        public List<CreateNewReportParameterValueRequest> Values { get; set; }
    }
}