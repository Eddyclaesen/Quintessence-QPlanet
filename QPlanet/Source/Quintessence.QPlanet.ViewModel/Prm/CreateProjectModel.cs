using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class CreateProjectModel
    {
        [Required]
        [Display(Name = "Project Name")]
        [MinLength(5)]
        public string ProjectName { get; set; }

        public Guid? MainProjectId { get; set; }

        public Guid? CopyProjectId { get; set; }

        [Required]
        [Display(Name = "Project Type")]
        public Guid ProjectTypeId { get; set; }

        public IEnumerable<ProjectTypeSelectListItemModel> ProjectTypes { get; set; }

        [Required]
        public int? ContactId { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public string ContactName { get; set; }

        [Display(Name = "Teamleader Project")]
        public int? CrmProjectId { get; set; }

        [Required]
        public Guid ProjectManagerUserId { get; set; }

        [Required]
        [Display(Name = "Project Manager")]
        public string ProjectManagerFullName { get; set; }

        [Required]
        public Guid CustomerAssistantUserId { get; set; }

        [Required]
        [Display(Name = "Customer Assistant")]
        public string CustomerAssistantFullName { get; set; }

        public ProjectView MainProject { get; set; }

        public Guid? ProjectCandidateId { get; set; }

        public ProjectView CopyProject { get; set; }
    }
}
