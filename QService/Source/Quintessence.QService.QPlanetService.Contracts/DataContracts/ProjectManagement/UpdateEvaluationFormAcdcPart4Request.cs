using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateEvaluationFormAcdcPart4Request : UpdateEvaluationFormRequestBase
    {
        [DataMember]
        public bool? Question19 { get; set; }

        [DataMember]
        public string Question19_19A { get; set; }

        [DataMember]
        public string Question19_19B { get; set; }

        [DataMember]
        public string Question20 { get; set; }

        [DataMember]
        public string Question21 { get; set; }
    }
}