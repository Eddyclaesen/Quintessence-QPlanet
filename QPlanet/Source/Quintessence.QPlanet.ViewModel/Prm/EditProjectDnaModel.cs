using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Base;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectDnaModel : BaseEntityModel
    {
        private List<EditProjectDnaCommercialTranslationModel> _projectDnaCommercialTranslations;
        public string CrmProjectName { get; set; }

        [AllowHtml]
        public string Details { get; set; }

        public int CrmProjectId { get; set; }

        [Display(Name = "Approved for references")]
        public bool ApprovedForReferences { get; set; }

        public List<EditProjectDnaCommercialTranslationModel> ProjectDnaCommercialTranslations
        {
            get
            {
                if (_projectDnaCommercialTranslations != null)
                    return _projectDnaCommercialTranslations.OrderBy(t => t.LanguageId).ToList();
                return _projectDnaCommercialTranslations;
            }
            set { _projectDnaCommercialTranslations = value; }
        }

        [Display(Name = "Type of project")]
        public List<EditProjectDnaSelectedTypeModel> ProjectDnaTypes { get; set; }

        [Display(Name = "Contacts of project")]
        public List<EditProjectDnaSelectedContactModel> ProjectDnaContactPersons { get; set; }

        public string ContactFullName { get; set; }
    }
}