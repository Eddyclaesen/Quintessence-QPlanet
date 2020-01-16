using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectRoleRequest : UpdateRequestBase
    {
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int SurveyPlanningId { get; set; }

        [DataMember]
        public List<UpdateProjectRoleTranslationRequest> ProjectRoleTranslations { get; set; }
    }
}
