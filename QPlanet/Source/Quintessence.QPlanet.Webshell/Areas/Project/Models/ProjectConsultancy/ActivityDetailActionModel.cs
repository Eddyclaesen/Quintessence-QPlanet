using System.Collections.Generic;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Scm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectConsultancy
{
    public class ActivityDetailActionModel
    {
        public ConsultancyProjectView Project { get; set; }

        public List<ActivityView> Activities { get; set; }

        public bool IsCurrentUserProjectManager { get; set; }

        public bool IsCurrentUserCustomerAssistant { get; set; }
    }
}