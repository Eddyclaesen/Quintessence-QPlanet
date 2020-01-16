using System.Collections.Generic;
using Quintessence.QPlanet.ViewModel.Prm;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectConsultancy
{
    public class ProjectPlanPhasesActionModel
    {
        public ProjectView Project { get; set; }

        public List<EditProjectPlanPhaseModel> ProjectPlanPhases { get; set; }

        public List<ProjectFixedPriceView> ProjectFixedPrices { get; set; }
    }
}