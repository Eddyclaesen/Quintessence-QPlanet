using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.Infrastructure.Web;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Sim;

namespace Quintessence.QPlanet.ViewModel.Prm.ProjectCategoryDetail
{
    public class EditProjectCategoryDetailModelBase : BaseEntityModel
    {
        public Guid ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string ProjectTypeCategoryName { get; set; }

        public int ProjectContactId { get; set; }

        public bool ProjectHasSubProjectCategoryDetails { get; set; }

        public List<SimulationContextView> SimulationContexts { get; set; }

        [Display(Name = "Remarks feedback")]
        public string FeedbackCustomerPhoneRemarks { get; set; }

        [Display(Name = "Remarks report")]
        public string FeedbackCustomerReportRemarks { get; set; }

        [Display(Name = "Revise by PM required")]
        public bool FeedbackCustomerReportReviseByProjectManager { get; set; }

        [Display(Name = "Simulation context")]
        public Guid? SimulationContextId { get; set; }

        [Display(Name = "Simulation remarks")]
        public string SimulationRemarks { get; set; }

        [Display(Name = "Matrix remarks")]
        public string MatrixRemarks { get; set; }

        //public List<ProjectRoleSelectListItem> ProjectRoles { get; set; }
        public List<DropDownListGroupItem> ProjectRoles { get; set; }

        [Display(Name = "Scoring")]
        public int ScoringTypeCode { get; set; }

        public IEnumerable<SelectListItem> CreateScoringTypeModelSelectListItems(int selectedScoringTypeCode)
        {
            yield return new SelectListItem { Selected = selectedScoringTypeCode == (int)ScoringTypeCodeType.WithIndicators, Value = ((int)ScoringTypeCodeType.WithIndicators).ToString(CultureInfo.InvariantCulture) , Text = EnumMemberNameAttribute.GetName(ScoringTypeCodeType.WithIndicators)};
            yield return new SelectListItem { Selected = selectedScoringTypeCode == (int)ScoringTypeCodeType.WithoutIndicators, Value = ((int)ScoringTypeCodeType.WithoutIndicators).ToString(CultureInfo.InvariantCulture), Text = EnumMemberNameAttribute.GetName(ScoringTypeCodeType.WithoutIndicators) };
        }

        public IEnumerable<SelectListItem> CreateSimulationContextDropDownList(Guid? simulationContextId)
        {
            yield return new SelectListItem { Selected = !simulationContextId.HasValue, Text = string.Empty, Value = "null" };
            foreach (var simulationContext in SimulationContexts.OrderBy(sc => sc.Name))
                yield return new SelectListItem { Selected = simulationContext.Id == simulationContextId, Text = simulationContext.Name, Value = simulationContext.Id.ToString() };
        }
    }
}
