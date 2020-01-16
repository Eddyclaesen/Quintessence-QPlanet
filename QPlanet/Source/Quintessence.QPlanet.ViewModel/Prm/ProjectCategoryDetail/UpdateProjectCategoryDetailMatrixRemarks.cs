using System;
using System.Web.Mvc;

namespace Quintessence.QPlanet.ViewModel.Prm.ProjectCategoryDetail
{
    public class UpdateProjectCategoryDetailMatrixRemarks
    {
        public Guid ProjectCategoryDetailId { get; set; }

        public int ScoringTypeCode { get; set; }

        [AllowHtml]
        public string MatrixRemarks { get; set; }
    }
}
