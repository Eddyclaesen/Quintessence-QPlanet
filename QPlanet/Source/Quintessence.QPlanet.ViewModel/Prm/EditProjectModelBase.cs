using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QPlanet.ViewModel.Crm;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectModelBase : BaseEntityModel
    {
        [Required]
        [Display(Name = "Project Name")]
        [MinLength(5)]
        public string Name { get; set; }

        [Display(Name = "Customer")]
        public string ContactFullName { get; set; }

        [Required]
        public Guid ProjectManagerId { get; set; }

        public Guid? CoProjectManagerId { get; set; }

        [Display(Name = "Pricing Model")]
        public int PricingModelId { get; set; }

        public PricingModelType PricingModelType { get { return (PricingModelType)PricingModelId; } }

        [Required]
        public Guid? CustomerAssistantId { get; set; }

        [Required]
        [Display(Name = "Project Manager")]
        public string ProjectManagerFullName { get; set; }

        [Display(Name = "Co-Project Manager")]
        public string CoProjectManagerFullName { get; set; }

        [Required]
        [Display(Name = "Customer Assistant")]
        public string CustomerAssistantFullName { get; set; }

        [Display(Name = "Project Type")]
        public string ProjectTypeName { get; set; }

        public string ProjectTypeCode { get; set; }

        [Display(Name = "Status")]
        public int StatusCode { get; set; }

        public ProjectStatusCodeViewType Status { get { return (ProjectStatusCodeViewType)StatusCode; } }

        public int ContactId { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        public ContactDetailModel ContactDetail { get; set; }

        [Display(Name = "Department information")]
        public string DepartmentInformation { get; set; }

        [Display(Name = "Confidential")]
        public bool Confidential { get; set; }

        [Display(Name = "ROI")]
        public bool ROI { get; set; }

        [Display(Name = "Executive")]
        public bool Executive { get; set; }

        [Display(Name = "Lock")]
        public bool Lock { get; set; }

        public IEnumerable<SelectListItem> CreatePricingModelSelectListItems(int pricingModelId)
        {
            switch (Status)
            {
                case ProjectStatusCodeViewType.Draft:
                    return Enum.GetValues(typeof(PricingModelType)).OfType<PricingModelType>().Select(pmt => new SelectListItem
                    {
                        Selected = (int)pmt == pricingModelId,
                        Value = ((int)pmt).ToString(CultureInfo.InvariantCulture),
                        Text = EnumMemberNameAttribute.GetName(pmt)
                    });

                default:
                    return Enum.GetValues(typeof(PricingModelType)).OfType<PricingModelType>().Where(pmt => (int)pmt == pricingModelId).Select(pmt => new SelectListItem
                    {
                        Selected = (int)pmt == pricingModelId,
                        Value = ((int)pmt).ToString(CultureInfo.InvariantCulture),
                        Text = EnumMemberNameAttribute.GetName(pmt)
                    });
            }
        }
    }
}
