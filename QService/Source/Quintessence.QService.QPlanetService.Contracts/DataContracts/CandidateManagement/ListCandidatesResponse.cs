using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;
using Quintessence.QService.QueryModel.Cam;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.CandidateManagement
{
    [DataContract]
    public class ListCandidatesResponse : ListResponseBase
    {
        [DataMember]
        public List<CandidateView> Candidates { get; set; }
    }
}
