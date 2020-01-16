using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class CreateNewProjectModel
    {
        [Display(Name = "Project name")]
        public string Name { get; set; }

        [Display(Name = "Select a project name")]
        public Guid SelectedProjectTypeId { get; set; }

        public IEnumerable<ProjectTypeSelectListItemModel> ProjectTypes { get; set; }

        [Display(Name = "Select a customer")]
        public int SelectedContactId { get; set; }

        [Display(Name = "Select a TL project")]
        public int SelectedCrmProjectId { get; set; }

        [Display(Name = "Select a project manager")]
        public Guid? SelectedProjectManagerUserId { get; set; }

        [Display(Name = "Select a customer assistant")]
        public Guid? SelectedCustomerAssistantUserId { get; set; }
        
        public string SelectedProjectManagerFullName { get; set; }
    }
}
