using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateEvaluationFormCoachingPart2Request : UpdateEvaluationFormRequestBase
    {
        [DataMember]
        public bool? Question03 { get; set; }

        [DataMember]
        public bool? Question04A { get; set; }

        [DataMember]
        public string Question04A_41A_411 { get; set; }

        [DataMember]
        public string Question04A_41A_412 { get; set; }

        [DataMember]
        public string Question04A_41A_413 { get; set; }

        [DataMember]
        public string Question04A_41B_411 { get; set; }

        [DataMember]
        public string Question04A_41B_412 { get; set; }

        [DataMember]
        public string Question04B { get; set; }
    }
}