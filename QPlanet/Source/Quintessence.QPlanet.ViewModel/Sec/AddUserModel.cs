using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Inf;

namespace Quintessence.QPlanet.ViewModel.Sec
{
    public class AddUserModel
    {
        public List<LanguageView> Languages { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Language")]
        public int LanguageId { get; set; }

        public IEnumerable<SelectListItem> CreateLanguageDropdownList()
        {
            return Languages.AsSelectListItems(l => l.Id.ToString(), l => l.Name, l => l.Id == LanguageId);
        }
    }
}