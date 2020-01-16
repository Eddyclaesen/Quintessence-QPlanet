using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Inf;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QPlanet.ViewModel.Sec
{
    public class EditUserModel : BaseEntityModel
    {
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Reset password")]
        public string ResetPassword { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Language")]
        public int LanguageId { get; set; }

        [Display(Name = "Employee")]
        public bool IsEmployee { get; set; }

        [Display(Name = "Account locked")]
        public bool IsLocked { get; set; }

        public string FullName { get; set; }

        [Display(Name = "Role")]
        public Guid? RoleId { get; set; }

        public List<RoleView> Roles { get; set; }

        public IEnumerable<SelectListItem> CreateRoleDropdownList()
        {
            var list = Roles.AsSelectListItems(l => l.Id.ToString(), l => l.Name, l => l.Id == RoleId).ToList();
            list.Insert(0, new SelectListItem { Selected = RoleId == null, Value = null, Text = string.Empty });
            return list;
        }

        public List<LanguageView> Languages { get; set; }

        public IEnumerable<SelectListItem> CreateLanguageDropdownList()
        {
            return Languages.AsSelectListItems(l => l.Id.ToString(), l => l.Name, l => l.Id == LanguageId);
        }
    }
}
