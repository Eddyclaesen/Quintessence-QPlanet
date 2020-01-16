using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Scm
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public Guid ProductTypeId { get; set; }
        public Guid ProjectId { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
    }
}