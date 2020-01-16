using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateEvaluationFormAcdcPart1Request : UpdateEvaluationFormRequestBase
    {
        [DataMember]
        public int? Question01 { get; set; }

        [DataMember]
        public int? Question02 { get; set; }

        [DataMember]
        public int? Question03 { get; set; }

        [DataMember]
        public int? Question04 { get; set; }

        [DataMember]
        public int? Question05 { get; set; }

        [DataMember]
        public int? Question06 { get; set; }

        [DataMember]
        public int? Question07 { get; set; }

        [DataMember]
        public string Question08 { get; set; }
    }
}