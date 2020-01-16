using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement
{
    [DataContract]
    public class UpdateReportParameterRequest : UpdateRequestBase
    {
        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string DefaultText { get; set; }

        [DataMember]
        public List<UpdateReportParameterValueRequest> ReportParameterValues { get; set; }
    }
}