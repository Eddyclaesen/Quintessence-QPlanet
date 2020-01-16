using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class FrameworkAgreement : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ContactId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
