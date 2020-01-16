using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateEvaluationFormAcdcPart5Request : UpdateEvaluationFormRequestBase
    {
        [DataMember]
        public string Question22 { get; set; }

        [DataMember]
        public string Question23 { get; set; }
    }
}