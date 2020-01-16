using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectTypeCategoryUnitPrice : EntityBase
    {
        public Guid ProjectTypeCategoryId { get; set; }
        public Guid ProjectTypeCategoryLevelId { get; set; }
        public decimal UnitPrice { get; set; }
    }
}