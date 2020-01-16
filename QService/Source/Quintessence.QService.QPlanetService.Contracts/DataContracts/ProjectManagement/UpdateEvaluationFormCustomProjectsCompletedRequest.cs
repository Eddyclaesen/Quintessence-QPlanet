using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateEvaluationFormCustomProjectsCompletedRequest : UpdateEvaluationFormRequestBase
    {
        [DataMember]
        public bool IsCompleted { get; set; }
    }
}