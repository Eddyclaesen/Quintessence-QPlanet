using Quintessence.QPlanet.ViewModel.Prm.ProjectCategoryDetail.Interfaces;

namespace Quintessence.QPlanet.ViewModel.Prm.ProjectCategoryDetail
{
    public class EditProjectCategoryEaDetailModel : EditProjectCategoryDetailModelBase, IIndicatorSelectionModel, ISimulationSelectionModel
    {
        public ProjectCategoryDetailProject Project { get; set; }
    }
}