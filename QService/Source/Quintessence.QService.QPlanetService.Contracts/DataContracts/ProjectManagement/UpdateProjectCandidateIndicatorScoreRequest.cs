using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectCandidateIndicatorScoreRequest : UpdateRequestBase
    {
        [DataMember]
        public decimal? Score { get; set; }
    }
}