using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateEvaluationFormAcdcPart6Request : UpdateEvaluationFormRequestBase
    {
        [DataMember]
        public string Question24 { get; set; }

        [DataMember]
        public string Question25 { get; set; }

        [DataMember]
        public string Question26 { get; set; }	
    }
}