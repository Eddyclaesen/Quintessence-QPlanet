using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateEvaluationFormAcdcPart2Request : UpdateEvaluationFormRequestBase
    {
        [DataMember]
        public string Question09 { get; set; }

        [DataMember]
        public string Question10 { get; set; }

        [DataMember]
        public bool? Question11 { get; set; }

        [DataMember]
        public bool? Question12 { get; set; }

        [DataMember]
        public bool? Question13 { get; set; }

        [DataMember]
        public bool? Question14 { get; set; }

        [DataMember]
        public bool? Question15 { get; set; }

        [DataMember]
        public bool? Question16 { get; set; }
    }
}