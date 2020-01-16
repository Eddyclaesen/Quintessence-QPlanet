using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Rep
{
    public class EditReportParameterValueModel : BaseEntityModel
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }

        [AllowHtml]
        public string Text { get; set; }
    }
}