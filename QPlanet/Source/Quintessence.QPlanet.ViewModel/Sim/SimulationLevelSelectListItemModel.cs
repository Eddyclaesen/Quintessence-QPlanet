using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QPlanet.ViewModel.Sim
{
    public class SimulationLevelSelectListItemModel : NullableGuidSelectListModel
    {
        public SimulationLevelSelectListItemModel()
            : base()
        {
        }

        public SimulationLevelSelectListItemModel(IViewEntity viewEntity)
            : base(viewEntity, e => e.Name)
        {
        }
    }
    public class CandidateReportDefinitionSelectListItemModel : NullableGuidSelectListModel
    {
        public CandidateReportDefinitionSelectListItemModel()
            : base()
        {
        }

        public CandidateReportDefinitionSelectListItemModel(IViewEntity viewEntity)
            : base(viewEntity, e => e.Name)
        {
        }
    }
}
