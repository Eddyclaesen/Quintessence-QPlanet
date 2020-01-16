using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectPlanPhaseActivityRequest : UpdateProjectPlanPhaseEntryRequest
    {
        [DataMember]
        public decimal Duration { get; set; }

        [DataMember]
        public string Notes { get; set; }
    }
}