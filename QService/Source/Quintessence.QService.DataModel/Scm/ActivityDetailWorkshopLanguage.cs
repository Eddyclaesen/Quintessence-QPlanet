using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Scm
{
    public class ActivityDetailWorkshopLanguage : EntityBase
    {
        public Guid ActivityDetailWorkshopId { get; set; }
        public int LanguageId { get; set; }
        public int SessionQuantity { get; set; }
    }
}