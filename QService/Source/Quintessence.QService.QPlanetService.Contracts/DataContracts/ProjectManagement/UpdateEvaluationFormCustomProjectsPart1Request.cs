using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateEvaluationFormCustomProjectsPart1Request : UpdateEvaluationFormRequestBase
    {
        [DataMember]
        public string Question01 { get; set; }

        [DataMember]
        public string Question02 { get; set; }

        [DataMember]
        public string Question03 { get; set; }
    }
}