using System;

namespace Quintessence.QPlanet.ViewModel.Sim
{
    public class SimulationLanguageModel
    {
        private bool? _isChecked;

        public bool IsChecked
        {
            get { return _isChecked.GetValueOrDefault(SimulationCombinationId.HasValue); }
            set { _isChecked = value; }
        }

        public int LanguageId { get; set; }

        public string LanguageName { get; set; }

        public Guid? SimulationCombinationId { get; set; }
    }
}
