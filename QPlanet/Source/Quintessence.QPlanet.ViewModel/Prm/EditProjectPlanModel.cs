using System.Collections.Generic;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectPlanModel : BaseEntityModel
    {
        public List<EditProjectPlanPhaseModel> ProjectPlanPhases { get; set; }
    }
}
