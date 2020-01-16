using Quintessence.QPlanet.ViewModel.Prm;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectCandidateOverview
{
    public class ProjectCandidateOverviewVersionMismatchActionModel
    {
        public EditProjectCandidateOverviewEntryFieldModel CurrentVersion { get; set; }
        public ProjectCandidateOverviewEntryView ServerVersion { get; set; }        

        public string GetServerPropertyValue()
        {
            var property = ServerVersion.GetType().GetProperty(CurrentVersion.PropertyName);
            return property.GetValue(ServerVersion).ToString();
        }
    }
}