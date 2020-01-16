using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Inf
{
    public class MailTemplateTranslation : EntityBase
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public int LanguageId { get; set; }
        public Guid MailTemplateId { get; set; }
    }
}