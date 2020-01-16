using System;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectCategoryAcDetail : ProjectCategoryDetail
    {
        public int ScoringTypeCode { get; set; }
        public string SimulationRemarks { get; set; }
        public Guid? SimulationContextId { get; set; }
        public string MatrixRemarks { get; set; }
    }
}