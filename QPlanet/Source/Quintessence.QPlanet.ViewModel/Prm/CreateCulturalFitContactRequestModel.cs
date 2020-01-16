using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Crm;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class CreateCulturalFitContactRequestModel
    {
        public int ContactId { get; set; }

        public Guid ProjectId { get; set; }

        [Display(Name = "Contact person")]
        public int CrmEmailId { get; set; }

        [Display(Name = "Deadline")]
        public DateTime Deadline { get; set; }

        [Display(Name = "Type")]
        public int TheoremListRequestTypeId { get; set; }

        public List<CrmEmailView> ContactPersons { get; set; }

        public string ContactName { get; set; }

        public string ProjectName { get; set; }

        public string Description { get; set; }

        public IEnumerable<SelectListItem> CreateContactPersonDropDownList(int crmEmailId)
        {
            yield return new SelectListItem { Selected = true, Value = "-1", Text = string.Empty };

            foreach (var contactPerson in ContactPersons)
            {
                yield return new SelectListItem
                {
                    Selected = contactPerson.Id == crmEmailId,
                    Value = contactPerson.Id.ToString(CultureInfo.InvariantCulture),
                    Text = string.Format("{0}, {1}", contactPerson.LastName, contactPerson.FirstName)
                };
            }
        }

        public List<SelectListItem> CreateTheoremListRequestTypeSelectListItems(int theoremListRequestTypeId)
        {
            return Enum.GetValues(typeof(TheoremListRequestType)).OfType<TheoremListRequestType>().Select(type =>
                                                                                            new SelectListItem
                                                                                            {
                                                                                                Value = ((int) type).ToString(CultureInfo.InvariantCulture),
                                                                                                Text = EnumMemberNameAttribute.GetName(type),
                                                                                                Selected = (int) type == theoremListRequestTypeId
                                                                                            }).ToList();
        }
        
    }
}