using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectEvaluationView : ViewEntityBase
    {
        [DataMember]
        public int CrmProjectId { get; set; }

        [DataMember]
        public string LessonsLearned { get; set; }

        [DataMember]
        public string Evaluation { get; set; }
    }
}