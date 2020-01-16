using System;
using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class ProjectAssessmentDevelopmentModel : BaseEntityModel
    {
        [Display(Name = "Project name")]
        public string Name { get; set; }

        [Display(Name = "Customer")]
        public string ContactFullName { get; set; }

        public Guid ProjectManagerId { get; set; }

        public Guid? CustomerAssistantId { get; set; }

        public string ProjectManagerFullName { get; set; }

        public string CustomerAssistantFullName { get; set; }
    }
}
