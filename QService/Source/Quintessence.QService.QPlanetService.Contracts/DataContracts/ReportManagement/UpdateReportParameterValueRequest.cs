using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement
{
    [DataContract]
    public class UpdateReportParameterValueRequest : UpdateRequestBase
    {
        [DataMember]
        public string Text { get; set; }
    }
}