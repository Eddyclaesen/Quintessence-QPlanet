using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class CreateProjectEvaluationRequest
    {
        [DataMember]
        public int CrmProjectId { get; set; }

        [DataMember]
        public string LessonsLearned { get; set; }

        [DataMember]
        public string Evaluation { get; set; }
    }
}