using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Scm
{
    public class ActivityDetailTrainingLanguage : EntityBase
    {
        public Guid ActivityDetailTrainingId { get; set; }
        public int LanguageId { get; set; }
        public int SessionQuantity { get; set; }
    }
}