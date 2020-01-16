using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    
    public class EditProjectCandidateCategoryDetailTypeModel
    {
        public Guid Id { get; set; }

        private List<SelectListItem> _offices;

        public Guid ProjectCandidateId { get; set; }

        public Guid ProjectCategoryDetailTypeId { get; set; }

        public string ProjectCategoryDetailTypeName { get; set; }

        public string ProjectCategoryDetailTypeCode { get; set; }

        [Display(Name = "Survey moment")]
        public int SurveyPlanningId { get; set; }

        [Display(Name="Invoice amount")]
        public decimal InvoiceAmount { get; set; }

        public int InvoiceStatusCode { get; set; }

        public int PricingModelId { get; set; }

        public string SurveyPlanningName
        {
            get { return EnumMemberNameAttribute.GetName(Enum.GetValues(typeof(SurveyPlanningType)).OfType<SurveyPlanningType>().FirstOrDefault(spt => (int)spt == SurveyPlanningId)); }
            }

        public string DetailType { get { return GetType().FullName; } }

        public List<SelectListItem> Offices
        {
            get { return _offices ?? (_offices = new List<SelectListItem>()); }
        }
    }
}