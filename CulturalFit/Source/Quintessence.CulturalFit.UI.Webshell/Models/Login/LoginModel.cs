using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quintessence.CulturalFit.UI.Webshell.Models.Login
{
    public class LoginModel
    {
        [Display(Name = "Code")]
        public string VerificationCode { get; set; }

        public bool QuestionnaireCompleted { get; set; }

        public List<LanguageEntity> Languages { get; set; }

        public string SelectedLanguage { get; set; }
    }
}
