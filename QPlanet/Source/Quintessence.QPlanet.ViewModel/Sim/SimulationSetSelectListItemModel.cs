using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QPlanet.ViewModel.Sim
{
    public class SimulationSetSelectListItemModel : GuidSelectListModel
    {
        public SimulationSetSelectListItemModel(IViewEntity entity)
            : base(entity, e => e.Name)
        {

        }
    }
}
