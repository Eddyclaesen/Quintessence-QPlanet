using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Crm
{
    public class ContactDetail : EntityBase
    {
        public int ContactId { get; set; }

        public string Remarks { get; set; }
    }
}
