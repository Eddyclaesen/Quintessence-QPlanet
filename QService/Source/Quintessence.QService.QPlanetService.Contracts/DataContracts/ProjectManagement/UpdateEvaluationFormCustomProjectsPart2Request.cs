using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateEvaluationFormCustomProjectsPart2Request : UpdateEvaluationFormRequestBase
    {
        [DataMember]
        public string Question04 { get; set; }

        [DataMember]
        public string Question05 { get; set; }

        [DataMember]
        public string Question06 { get; set; }

        [DataMember]
        public string Question07 { get; set; }

        [DataMember]
        public string Question08 { get; set; }

        [DataMember]
        public string Question09 { get; set; }
    }
}