using System.Web.Mvc;

namespace Quintessence.QPlanet.ViewModel.Rep
{
    public class CreateNewReportParameterValueModel
    {
        public int LanguageId { get; set; }

        public string LanguageName { get; set; }

        [AllowHtml]
        public string Text { get; set; }
    }
}