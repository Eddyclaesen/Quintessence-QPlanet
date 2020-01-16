using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateEvaluationFormAcdcPart3Request : UpdateEvaluationFormRequestBase
    {
        [DataMember]
        public bool? Question17 { get; set; }

        [DataMember]
        public string Question18 { get; set; }
    }
}