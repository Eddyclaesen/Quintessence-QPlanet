using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QPlanet.ViewModel.Sim
{
    public class SimulationSelectListItemModel : GuidSelectListModel
    {
        public SimulationSelectListItemModel(IViewEntity entity)
            : base(entity, e => e.Name)
        {

        }
    }
}
