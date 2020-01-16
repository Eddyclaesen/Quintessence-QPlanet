using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateEvaluationFormCustomProjectsPart3Request : UpdateEvaluationFormRequestBase
    {
        [DataMember]
        public string Question10 { get; set; }

        [DataMember]
        public string Question11 { get; set; }

        [DataMember]
        public string Question12 { get; set; }

        [DataMember]
        public string Question13 { get; set; }

        [DataMember]
        public string Question14 { get; set; }

        [DataMember]
        public string Question15 { get; set; }
    }
}