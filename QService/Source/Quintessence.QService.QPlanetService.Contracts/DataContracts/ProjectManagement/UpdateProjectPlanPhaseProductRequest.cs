using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectPlanPhaseProductRequest : UpdateProjectPlanPhaseEntryRequest
    {
        [DataMember]
        public string Notes { get; set; }
    }
}