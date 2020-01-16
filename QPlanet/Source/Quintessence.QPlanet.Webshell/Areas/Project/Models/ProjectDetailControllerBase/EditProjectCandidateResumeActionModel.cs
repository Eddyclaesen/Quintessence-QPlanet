using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Quintessence.QPlanet.ViewModel.Prm;
using Quintessence.QPlanet.Webshell.Areas.Project.Models.Shared;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Rep;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectDetailControllerBase
{
    public class EditProjectCandidateResumeActionModel
    {
        public ProjectCandidateView ProjectCandidate { get; set; }

        public ProjectView Project { get; set; }

        public List<ProjectCandidateCompetenceSimulationScoreView> ProjectCandidateCompetenceSimulationScores { get; set; }

        public List<ProjectCandidateCompetenceScoreView> ProjectCandidateCompetenceScores { get; set; }

        public List<AdviceView> Advices { get; set; }

        public EditProjectCandidateResumeModel ProjectCandidateResume { get; set; }

        public CandidateReportDefinitionView ReportDefinition { get; set; }

        public int ProjectCandidateResumeAdviceId
        {
            get { return ProjectCandidateResume == null ? -1 : ProjectCandidateResume.AdviceId; }
            set { (ProjectCandidateResume ?? (ProjectCandidateResume = new EditProjectCandidateResumeModel())).AdviceId = value; }
        }

        public List<ProjectCandidateClusterScoreView> ProjectCandidateClusterScores { get; set; }

        public IEnumerable<SimulationModel> GetSimulations()
        {
            return ProjectCandidateCompetenceSimulationScores
                    .Select(Mapper.DynamicMap<SimulationModel>)
                    .Distinct(new SimulationModelComparer());
        }
    }
}