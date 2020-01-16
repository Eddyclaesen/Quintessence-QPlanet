using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateEvaluationFormCoachingPart5Request : UpdateEvaluationFormRequestBase
    {
        [DataMember]
        public bool? Question10 { get; set; }

        [DataMember]
        public string Question10_10A { get; set; }

        [DataMember]
        public string Question10_10B { get; set; }

        [DataMember]
        public string Question11 { get; set; }	
    }
}