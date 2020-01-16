using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement
{
    [DataContract]
    public class CreateNewReportParameterValueRequest
    {
        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public Guid? ReportParameterId { get; set; }
    }
}