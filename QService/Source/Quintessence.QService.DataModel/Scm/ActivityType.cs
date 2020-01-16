using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Scm
{
    public class ActivityType : EntityBase
    {
        public string Name { get; set; }

        public bool IsSystem { get; set; }
    }
}
