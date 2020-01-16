using System.Collections.Generic;
using Quintessence.QPlanet.ViewModel.Prm;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Sof;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectGeneral
{
    public class ListCrmProjectsActionModel
    {
        public List<CrmProjectView> CrmProjects { get; set; }

        public List<ProjectRevenueDistributionView> ProjectRevenueDistribution { get; set; }

        public List<CrmProjectRevenueDistributionModel> CrmProjectRevenueDistributions { get; set; }
    }
}