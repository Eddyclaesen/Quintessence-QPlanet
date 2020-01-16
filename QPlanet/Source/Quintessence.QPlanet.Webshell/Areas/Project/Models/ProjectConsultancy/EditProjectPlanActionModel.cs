using Quintessence.QPlanet.ViewModel.Prm;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectConsultancy
{
    public class EditProjectPlanActionModel
    {
        public ConsultancyProjectView Project { get; set; }

        public EditProjectPlanModel ProjectPlan { get; set; }

        public bool IsCurrentUserProjectManager { get; set; }

        public bool IsCurrentUserCustomerAssistant { get; set; }
    }
}
