using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    /// <summary>
    /// Base class for project category details
    /// </summary>
    public class ProjectCategoryDetail : EntityBase
    {
        public Guid ProjectId { get; set; }
        public Guid ProjectTypeCategoryId { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
