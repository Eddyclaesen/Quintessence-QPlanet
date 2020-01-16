using System;

namespace Quintessence.QPlanet.ViewModel.Dim
{
    public class DictionaryIndicatorMatrixEntryModel
    {
        public bool IsChecked { get; set; }

        public Guid Id { get; set; }

        public string DictionaryIndicatorName { get; set; }

        public int DictionaryIndicatorOrder { get; set; }
        
        public Guid DictionaryLevelId { get; set; }

        public string DictionaryLevelLevel { get; set; }

        public string DictionaryLevelName { get; set; }

        public Guid DictionaryCompetenceId { get; set; }

        public string DictionaryCompetenceName { get; set; }

        public int DictionaryCompetenceOrder { get; set; }

        public Guid DictionaryClusterId { get; set; }

        public string DictionaryClusterName { get; set; }

        public int DictionaryClusterOrder { get; set; }

        public Guid DictionaryId { get; set; }

        public string DictionaryName { get; set; }

        public string DictionaryIndicatorColor { get; set; }
    }
}
