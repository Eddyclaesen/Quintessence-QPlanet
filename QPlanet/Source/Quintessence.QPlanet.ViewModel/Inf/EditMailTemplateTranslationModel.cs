using System;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Inf
{
    public class EditMailTemplateTranslationModel : BaseEntityModel
    {
        [AllowHtml]
        public string Subject { get; set; }
        [AllowHtml]
        public string Body { get; set; }
        public int LanguageId { get; set; }
        public Guid MailTemplateId { get; set; }
        
    }
}