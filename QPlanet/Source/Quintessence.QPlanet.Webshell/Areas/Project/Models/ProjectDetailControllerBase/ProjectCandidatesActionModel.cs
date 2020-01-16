using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Prm;
using Quintessence.QPlanet.ViewModel.Prm.ProjectCategoryDetail;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Inf;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Rep;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectDetailControllerBase
{
    public class ProjectCandidatesActionModel
    {
        public List<ProjectCandidateView> Candidates { get; set; }
        public List<LanguageView> Languages { get; set; }
        public List<UserView> CustomerAssistants { get; set; }
        public List<ReportStatusView> ReportStatuses { get; set; }
        public List<ProjectCandidateView> DeletedAppointments { get; set; }
        public List<EditProjectCategoryDetailModel> ProjectCategoryDetails { get; set; }
        public List<AssessmentRoomView> AssessmentRooms { get; set; }
        public AssessmentDevelopmentProjectView Project { get; set; }
        public EditProjectCategoryDetailModelBase MainProjectCategoryDetail { get; set; }

        public List<ReportDefinitionView> ReportDefinitions { get; set; }

        public List<SelectListItem> CreateLanguageSelectListItems(int selectedLanguageId)
        {
            return Languages.Select(l => new SelectListItem { Selected = l.Id == selectedLanguageId, Text = l.Name, Value = l.Id.ToString() }).ToList();
        }

        public List<SelectListItem> CreateReportStatusSelectListItems(int selectedReportStatusId)
        {
            return ReportStatuses.Select(l => new SelectListItem { Selected = l.Id == selectedReportStatusId, Text = l.Name, Value = l.Id.ToString() }).ToList();
        }

        public List<SelectListItem> CreateGenderSelectListItems(string gender)
        {
            return Enum.GetValues(typeof(GenderType)).OfType<GenderType>().Select(g =>
                                                                                            new SelectListItem
                                                                                            {
                                                                                                Value = g.ToString(),
                                                                                                Text = EnumMemberNameAttribute.GetName(g),
                                                                                                Selected = g.ToString() == gender
                                                                                            }).ToList();
        }

        public List<SelectListItem> CreateCustomerAssistantSelectListItems(Guid? reportReviewerId)
        {
            var customerAssistants = new List<SelectListItem>
                {
                    new SelectListItem
                        {
                            Selected = reportReviewerId == null,
                            Text = "--",
                            Value = null
                        }
                };

            customerAssistants.AddRange(CustomerAssistants.Select(
                    c =>
                    new SelectListItem {Selected = c.Id == reportReviewerId, Text = c.FullName, Value = c.Id.ToString()})
                    .ToList());
            return customerAssistants;
        }

        public int RetrieveOfficeId(Guid assessmentRoomId)
        {
            var assessmentRoom = AssessmentRooms.FirstOrDefault(ar => ar.Id == assessmentRoomId);
            return assessmentRoom != null 
                ? assessmentRoom.OfficeId : 
                1;
        }
    }
}