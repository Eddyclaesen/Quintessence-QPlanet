using System;
using System.ComponentModel.DataAnnotations;

namespace Quintessence.QPlanet.ViewModel.Prm.ProjectCategoryDetail.Interfaces
{
    public interface IRoleSelectionModel
    {
        [Display(Name = "Project role")]
        string ProjectRoleName { get; set; }

        [Display(Name = "Project role")]
        Guid? ProjectRoleId { get; set; }

        ProjectCategoryDetailProject Project { get; set; }
    }
}
