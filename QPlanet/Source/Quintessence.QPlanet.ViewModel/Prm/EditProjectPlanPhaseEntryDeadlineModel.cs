using System;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectPlanPhaseEntryDeadlineModel : BaseEntityModel
    {
        public DateTime Deadline { get; set; }

        public string ProfileName { get; set; }

        public string ActivityName { get; set; }

        public string ProductTypeName { get; set; }

        public string ProductName { get; set; }

        public string Notes { get; set; }
    }
}