using System;
using System.Collections.Generic;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class FocusedScoresSimulationCombinationModel
    {
        private List<EditProjectCandidateCompetenceSimulationFocusedScoreModel> _competences;

        public Guid Id { get; set; }

        public Guid SimulationId { get; set; }

        public string SimulationName { get; set; }

        public Guid? SimulationLevelId { get; set; }

        public string SimulationLevelName { get; set; }

        public Guid? SimulationDepartmentId { get; set; }

        public string SimulationDepartmentName { get; set; }

        public Guid SimulationSetId { get; set; }

        public string SimulationSetName { get; set; }

        public List<EditProjectCandidateCompetenceSimulationFocusedScoreModel> Competences
        {
            get { return _competences ?? (_competences = new List<EditProjectCandidateCompetenceSimulationFocusedScoreModel>()); }
            set { _competences = value; }
        }

        public override string ToString()
        {
            var names = new List<string> { SimulationName };

            if (SimulationLevelId.HasValue)
                names.Add(SimulationLevelName);
            if (SimulationDepartmentId.HasValue)
                names.Add(SimulationDepartmentName);
            names.Add(SimulationSetName);

            return string.Join(" - ", names);
        }
    }
}