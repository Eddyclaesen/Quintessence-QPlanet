using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Dim;
using Quintessence.QService.QueryModel.Inf;
using Quintessence.QService.QueryModel.Sof;

namespace Quintessence.QPlanet.Webshell.Areas.Admin.Models.AdminDictionary
{
    public class ImportDictionaryActionModel
    {
        private List<ImportDictionaryMessageActionModel> _messages;
        public EditImportDictionaryModel Dictionary { get; set; }

        public List<CrmContactView> Contacts { get; set; }

        public List<LanguageView> Languages { get; set; }

        public List<ImportDictionaryMessageActionModel> Messages
        {
            get { return _messages ?? (_messages = new List<ImportDictionaryMessageActionModel>()); }
            set { _messages = value; }
        }

        public IEnumerable<SelectListItem> CreateContactSelectListItem()
        {
            return Contacts.OrderBy(c => c.FullName).AsSelectListItems(c => c.Id.ToString(), c => c.FullName);
        }
    }
}