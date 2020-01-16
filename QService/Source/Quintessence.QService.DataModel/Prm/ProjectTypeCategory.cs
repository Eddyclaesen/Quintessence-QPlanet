using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectTypeCategory : EntityBase
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public int? CrmTaskId { get; set; }

        public string Color { get; set; }

        public int? SubCategoryType { get; set; }

        public int? Execution { get; set; }
    }
}
