using System;
using Quintessence.QService.DataModel.Prm.Interfaces;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectCategoryFdDetail : ProjectCategoryDetail, IProjectCategoryDetailProjectRole
    {
        public int ScoringTypeCode { get; set; }
        public string SimulationRemarks { get; set; }
        public Guid? SimulationContextId { get; set; }
        public string MatrixRemarks { get; set; }
        public Guid? ProjectRoleId { get; set; }
    }
}