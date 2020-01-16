using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectDnaCommercialTranslationModel : BaseEntityModel
    {
        public string LanguageName { get; set; }

        [Display(Name = "Commercial name")]
        public string CommercialName { get; set; }

        [AllowHtml]
        [Display(Name = "Commercial recap")]
        public string CommercialRecap { get; set; }

        public int LanguageId { get; set; }
    }
}