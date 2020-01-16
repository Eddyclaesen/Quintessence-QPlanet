using System.Collections.Generic;
using Quintessence.QPlanet.ViewModel.Prm;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectDetailControllerBase
{
    public class EditProjectCandidateStandardSimulationScoresActionModel
    {
        private List<StandardScoresSimulationCombinationModel> _simulationCombinations;

        public ProjectCandidateView ProjectCandidate { get; set; }

        public List<StandardScoresSimulationCombinationModel> SimulationCombinations
        {
            get { return _simulationCombinations ?? (_simulationCombinations = new List<StandardScoresSimulationCombinationModel>()); }
            set { _simulationCombinations = value; }
        }

        public bool IsIndicatorScoringEnabled { get; set; }
    }
}