using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateEvaluationFormAcdcCompletedRequest : UpdateEvaluationFormRequestBase
    {
        [DataMember]
        public bool IsCompleted { get; set; }
    }
}