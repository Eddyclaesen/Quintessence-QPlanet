using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectEvaluationRequest : UpdateRequestBase
    {
        [DataMember]
        public int CrmProjectId { get; set; }

        [DataMember]
        public string LessonsLearned { get; set; }

        [DataMember]
        public string Evaluation { get; set; }
    }
}