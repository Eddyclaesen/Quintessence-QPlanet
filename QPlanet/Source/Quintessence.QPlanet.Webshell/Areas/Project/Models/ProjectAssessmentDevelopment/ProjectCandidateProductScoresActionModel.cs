using System.Collections.Generic;
using Quintessence.QPlanet.ViewModel.Prm;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectAssessmentDevelopment
{
    public class ProjectCandidateProductScoresActionModel
    {
        public List<NeopirScoreModel> NeopirScores { get; set; }
        public List<LeaderScoreModel> LeaderScores { get; set; }
        public List<ProjectCandidateRoiScoreView> RoiScores { get; set; }
        public ProjectCandidateView ProjectCandidate { get; set; }
        public ProjectView Project { get; set; }
        public bool MotivationInterview { get; set; }
    }
}