using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateEvaluationFormCoachingPart3Request : UpdateEvaluationFormRequestBase
    {
        [DataMember]
        public bool? Question05 { get; set; }

        [DataMember]
        public bool? Question05_5A { get; set; }

        [DataMember]
        public string Question05_5A_51A_511 { get; set; }

        [DataMember]
        public string Question05_5A_51A_512 { get; set; }

        [DataMember]
        public string Question05_5A_51A_513 { get; set; }

        [DataMember]
        public string Question05_5A_51B_511 { get; set; }

        [DataMember]
        public string Question05_5A_51B_512 { get; set; }

        [DataMember]
        public string Question05_5B { get; set; }	
    }
}