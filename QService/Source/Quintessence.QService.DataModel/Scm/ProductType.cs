using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Scm
{
    public class ProductType : EntityBase
    {	
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal UnitPrice{ get; set; }
    }
}