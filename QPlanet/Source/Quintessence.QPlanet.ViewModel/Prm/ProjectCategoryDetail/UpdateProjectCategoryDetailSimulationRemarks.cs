using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Quintessence.QPlanet.ViewModel.Prm.ProjectCategoryDetail
{
    public class UpdateProjectCategoryDetailSimulationRemarks
    {
        public Guid ProjectCategoryDetailId { get; set; }

        [Display(Name = "Simulation context")]
        public Guid? SimulationContextId { get; set; }

        [AllowHtml]
        public string SimulationRemarks { get; set; }
    }
}
