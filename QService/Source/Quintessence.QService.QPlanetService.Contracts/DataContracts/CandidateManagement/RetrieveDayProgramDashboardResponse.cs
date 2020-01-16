using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Cam;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.CandidateManagement
{
    [DataContract]
    public class RetrieveDayProgramDashboardResponse
    {
        [DataMember]
        public List<ProgramComponentView> ProgramComponents { get; set; }

        [DataMember]
        public DateTime Date { get; set; }
    }
}