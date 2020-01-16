using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement
{
    [DataContract]
    public class CreateNewCandidateReportDefinitionFieldRequest
    {
        [DataMember]
        public Guid CandidateReportDefinitionId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Code { get; set; }
    }
}