using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Inf;
using Quintessence.QService.QueryModel.Sof;
using Quintessence.QService.QueryModel.Wsm;

namespace Quintessence.QPlanet.ViewModel.Sim
{
    public class EditUserProfileModel : BaseEntityModel
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public int LanguageId { get; set; }

        public string LanguageCode { get; set; }

        public string LanguageName { get; set; }

        public List<LanguageView> Languages { get; set; }

        public List<CrmUserEmailView> Emails { get; set; }

        public List<UserProfileContactView> Contacts { get; set; }

        public IEnumerable<SelectListItem> CreateLanguageDropDownList(int languageId)
        {
            return Languages.Select(languageView => new SelectListItem
            {
                Selected = languageView.Id == languageId,
                Text = languageView.Name,
                Value = languageView.Id.ToString(CultureInfo.InvariantCulture)
            });
        }
    }
}