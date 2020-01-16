using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.CandidateManagement
{
    [DataContract]
    public class RetrieveDayProgramDashboardRequest
    {
        [DataMember]
        public DateTime Date { get; set; }
    }
}