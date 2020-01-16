using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateEvaluationFormCoachingPart6Request : UpdateEvaluationFormRequestBase
    {
        [DataMember]
        public int? Question12 { get; set; }

        [DataMember]
        public int? Question13 { get; set; }
    }
}