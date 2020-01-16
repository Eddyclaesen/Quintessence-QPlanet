using System.Collections.Generic;
using Quintessence.QPlanet.ViewModel.Prm;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectDetailControllerBase
{
    public class EditProjectCandidateFocusedSimulationScoresActionModel
    {
        private List<FocusedScoresSimulationCombinationModel> _simulationCombinations;

        public ProjectCandidateView ProjectCandidate { get; set; }

        public List<FocusedScoresSimulationCombinationModel> SimulationCombinations
        {
            get { return _simulationCombinations ?? (_simulationCombinations = new List<FocusedScoresSimulationCombinationModel>()); }
            set { _simulationCombinations = value; }
        }

        public bool IsIndicatorScoringEnabled { get; set; }
    }
}