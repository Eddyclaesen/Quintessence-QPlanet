using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;
using Quintessence.QService.QueryModel.Sim;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SimulationManagement
{
    [DataContract]
    public class ListSimulationSetsResponse : ListResponseBase
    {
        [DataMember]
        public List<SimulationSetView> SimulationSets { get; set; }
    }
}