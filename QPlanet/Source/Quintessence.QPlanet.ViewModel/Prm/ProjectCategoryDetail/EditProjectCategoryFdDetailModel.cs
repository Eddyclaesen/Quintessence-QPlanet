using System;
using Quintessence.QPlanet.ViewModel.Prm.ProjectCategoryDetail.Interfaces;

namespace Quintessence.QPlanet.ViewModel.Prm.ProjectCategoryDetail
{
    public class EditProjectCategoryFdDetailModel : EditProjectCategoryDetailModelBase, ISimulationSelectionModel, IRoleSelectionModel
    {
        public string ProjectRoleName { get; set; }
        public Guid? ProjectRoleId { get; set; }
        public ProjectCategoryDetailProject Project { get; set; }
    }
}