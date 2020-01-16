using System;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Rep;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class CandidateReportDefinitionCheckboxItemModel : CheckboxItemModelBase
    {
        public CandidateReportDefinitionCheckboxItemModel() { }

        public CandidateReportDefinitionCheckboxItemModel(CandidateReportDefinitionView candidateReportDefinition, bool isChecked = false)
            : this()
        {
            Id = candidateReportDefinition.Id;
            Name = candidateReportDefinition.Name;
            IsChecked = isChecked;
        }

        public Guid? Id { get; set; }

        public string Name { get; set; }
    }
}