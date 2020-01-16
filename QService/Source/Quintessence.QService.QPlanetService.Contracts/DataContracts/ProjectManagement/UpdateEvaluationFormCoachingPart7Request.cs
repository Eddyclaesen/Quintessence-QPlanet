using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateEvaluationFormCoachingPart7Request : UpdateEvaluationFormRequestBase
    {
        [DataMember]
        public string Question14 { get; set; }

        [DataMember]
        public string Question15 { get; set; }

        [DataMember]
        public bool? Question16 { get; set; }

        [DataMember]
        public string Question16_16A { get; set; }

        [DataMember]
        public string Question16_16B { get; set; }
    }
}