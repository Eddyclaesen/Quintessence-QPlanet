using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectCandidateIndicatorSimulationScoreRequest : UpdateRequestBase
    {
        [DataMember]
        public decimal? Score { get; set; }
    }
}