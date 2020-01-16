using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectTypeCategoryDefaultValue : EntityBase
    {
        public Guid ProjectTypeCategoryId { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
    }
}
