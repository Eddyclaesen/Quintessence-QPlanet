using Quintessence.QPlanet.ViewModel.Prm;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectCandidateReportingOverview
{
    public class ProjectCandidateReportingOverviewVersionMismatchActionModel
    {
        public EditProjectCandidateReportingOverviewEntryFieldModel CurrentVersion { get; set; }
        public ProjectCandidateReportingOverviewEntryView ServerVersion { get; set; }        

        public string GetServerPropertyValue()
        {
            var property = ServerVersion.GetType().GetProperty(CurrentVersion.PropertyName);
            return property.GetValue(ServerVersion).ToString();
        }
    }
}