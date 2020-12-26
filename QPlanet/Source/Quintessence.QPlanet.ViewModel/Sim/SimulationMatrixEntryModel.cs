using System;

namespace Quintessence.QPlanet.ViewModel.Sim
{
    public class SimulationMatrixEntryModel
    {
        public Guid SimulationSetId { get; set; }

        public string SimulationSetName { get; set; }

        public Guid? SimulationDepartmentId { get; set; }

        public string SimulationDepartmentName { get; set; }

        public Guid? SimulationLevelId { get; set; }

        public string SimulationLevelName { get; set; }

        public Guid SimulationId { get; set; }

        public string SimulationName { get; set; }

        public int Preparation { get; set; }

        public int Execution { get; set; }

        public bool IsChecked { get; set; }

        public Guid Id { get; set; }

        public string LanguageNames { get; set; }

        public int QCandidateLayoutId { get; set; }
    }
}
