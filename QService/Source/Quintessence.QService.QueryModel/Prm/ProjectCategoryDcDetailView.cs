using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectCategoryDcDetailView : ProjectCategoryDetailView
    {
        [DataMember]
        public int ScoringTypeCode { get; set; }

        [DataMember]
        public string SimulationRemarks { get; set; }

        [DataMember]
        public Guid? SimulationContextId { get; set; }

        [DataMember]
        public string MatrixRemarks { get; set; }
    }
}