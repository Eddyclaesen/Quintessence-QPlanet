using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class TheoremTemplateTranslation : EntityBase
    {
        public Guid TheoremTemplateId { get; set; }
        public int LanguageId { get; set; }
        public string Text { get; set; }
        public Boolean IsDefault { get; set; }
    }
}