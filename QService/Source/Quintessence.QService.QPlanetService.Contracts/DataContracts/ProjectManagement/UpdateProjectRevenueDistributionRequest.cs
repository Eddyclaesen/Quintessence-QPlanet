using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectRevenueDistributionRequest : UpdateRequestBase
    {
        [DataMember]
        public decimal? Revenue { get; set; }
    }
}