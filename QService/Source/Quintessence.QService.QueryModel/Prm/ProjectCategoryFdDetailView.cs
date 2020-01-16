using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectCategoryFdDetailView : ProjectCategoryDetailView
    {
        [DataMember]
        public int ScoringTypeCode { get; set; }

        [DataMember]
        public string SimulationRemarks { get; set; }

        [DataMember]
        public Guid? SimulationContextId { get; set; }

        [DataMember]
        public string MatrixRemarks { get; set; }

        [DataMember]
        public Guid? ProjectRoleId { get; set; }

        [DataMember]
        public ProjectRoleView ProjectRole { get; set; }
    }
}