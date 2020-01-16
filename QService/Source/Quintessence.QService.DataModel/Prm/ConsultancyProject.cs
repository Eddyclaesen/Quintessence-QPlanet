using System;

namespace Quintessence.QService.DataModel.Prm
{
    public class ConsultancyProject : Project
    {
        public string ProjectInformation { get; set; }
        public Guid ProjectPlanId { get; set; }
    }
}
