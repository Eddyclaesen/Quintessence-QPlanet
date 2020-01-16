using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Inf;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.InfrastructureManagement
{
    [DataContract]
    public class SearchResponse
    {
        [DataMember]
        public List<SearchResultView> Results { get; set; }
    }
}