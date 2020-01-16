using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QPlanet.ViewModel.Sim
{
    public class SimulationDepartmentSelectListItemModel : NullableGuidSelectListModel
    {
        public SimulationDepartmentSelectListItemModel()
            : base()
        {
        }

        public SimulationDepartmentSelectListItemModel(IViewEntity viewEntity)
            : base(viewEntity, e => e.Name)
        {
        }
    }
}
