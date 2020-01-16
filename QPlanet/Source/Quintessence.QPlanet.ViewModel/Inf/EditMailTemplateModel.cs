using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Inf;

namespace Quintessence.QPlanet.ViewModel.Inf
{
    public class EditMailTemplateModel : BaseEntityModel
    {
        [Required]
        [Display(Name="Name")]
        public string Name { get; set; }

        public string Code { get; set; }

        [Required]
        [Display(Name = "'From' email address")]
        public string FromAddress { get; set; }

        [Display(Name = "'BCC' email address(es)")]
        public string BccAddress { get; set; }

        public string StoredProcedureName { get; set; }

        public List<EditMailTemplateTranslationModel> MailTemplateTranslations { get; set; }

        public List<LanguageView> Languages { get; set; }

        public List<SelectListItem> CreateLanguageSelectListItems(int selectedLanguageId)
        {
            return Languages.Select(l => new SelectListItem { Selected = l.Id == selectedLanguageId, Text = l.Name, Value = l.Id.ToString(CultureInfo.InvariantCulture) }).ToList();
        }
     }
}
