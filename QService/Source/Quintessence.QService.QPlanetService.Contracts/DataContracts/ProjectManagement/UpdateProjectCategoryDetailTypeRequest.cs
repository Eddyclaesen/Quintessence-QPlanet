using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectCategoryDetailTypeRequest : UpdateRequestBase
    {
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int SurveyPlanningId { get; set; }

        [DataMember]
        public bool IncludeInCandidateReport { get; set; }
    }
}
