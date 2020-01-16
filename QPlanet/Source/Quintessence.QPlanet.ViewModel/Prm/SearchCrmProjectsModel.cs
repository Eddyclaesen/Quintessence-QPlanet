using System;
using System.ComponentModel.DataAnnotations;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class SearchCrmProjectsModel
    {
        [Display(Name = "Project name")]
        public string ProjectName { get; set; }

        [Display(Name = "Gepland")]
        public bool IsStatusPlannedChecked { get; set; }

        [Display(Name = "Lopend")]
        public bool IsStatusRunningChecked { get; set; }

        [Display(Name = "Voltooid")]
        public bool IsStatusDoneChecked { get; set; }

        [Display(Name = "Gestopt")]
        public bool IsStatusStoppedChecked { get; set; }

        public Guid? ProjectId { get; set; }
    }
}
