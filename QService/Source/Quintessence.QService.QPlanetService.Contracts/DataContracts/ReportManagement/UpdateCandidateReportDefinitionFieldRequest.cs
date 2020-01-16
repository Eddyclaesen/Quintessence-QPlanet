using System;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement
{
    [DataContract]
    public class UpdateCandidateReportDefinitionFieldRequest : UpdateRequestBase
    {
        [DataMember]
        public Guid CandidateReportDefinitionId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public bool IsActive { get; set; }
    }
}