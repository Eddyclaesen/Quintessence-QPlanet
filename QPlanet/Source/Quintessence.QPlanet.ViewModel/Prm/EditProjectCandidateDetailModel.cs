using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Base;
using Quintessence.QPlanet.ViewModel.Prm.ProjectCategoryDetail;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Crm;
using Quintessence.QService.QueryModel.Inf;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Rep;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditProjectCandidateDetailModel : BaseEntityModel
    {
        public Guid CandidateId { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string CandidateFirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string CandidateLastName { get; set; }

        [Required]
        [Display(Name = "Candidate e-mail")]
        public string CandidateEmail { get; set; }

        [Required]
        [Display(Name = "Candidate phone")]
        public string CandidatePhone { get; set; }

        [Required]
        [Display(Name = "Candidate gender")]
        public string CandidateGender { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        public int ProjectContactId { get; set; }

        public List<EditProjectCandidateCategoryDetailTypeModel> ProjectCandidateCategoryDetailTypes { get; set; }

        public string FullName
        {
            get { return string.Format("{0} {1}", CandidateFirstName, CandidateLastName); }
        }

        public bool ProjectCandidateDetailIsSuperofficeAppointmentDeleted { get; set; }

        [Display(Name="Assessment date")]
        public DateTime ProjectCandidateDetailAssessmentStartDate { get; set; }

        public DateTime ProjectCandidateDetailAssessmentEndDate { get; set; }

        [Display(Name="Lead assessor")]
        public string ProjectCandidateDetailLeadAssessorFirstName { get; set; }

        public string ProjectCandidateDetailLeadAssessorLastName { get; set; }

        public string Code { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        [Display(Name = "Report deadline")]
        public DateTime ReportDeadline { get; set; }

        [Display(Name="Co-assessors")]
        public List<CrmAssessorAppointmentView> ProjectCandidateDetailCoAssessors { get; set; }

        public List<ProjectCandidateProjectView> ProjectCandidateProjects { get; set; }

        [Display(Name = "Cultural fit requests")]
        public List<TheoremListRequestView> TheoremListRequests { get; set; }

        [Display(Name="Candidate language")]
        public int CandidateLanguageId { get; set; }

        [Display(Name = "Report language")]
        public int ReportLanguageId { get; set; }

        [Display(Name = "Report reviewer")]
        public Guid? ReportReviewerId { get; set; }

        [Display(Name = "Report status")]
        public int ReportStatusId { get; set; }

        public bool IsCancelled { get; set; }

        [Display(Name = "Cancel date")]
        public DateTime? CancelledDate { get; set; }

        [Display(Name = "Reason for cancel")]
        public string CancelledReason { get; set; }

        [Display(Name = "Invoice amount")]
        public decimal? InvoiceAmount { get; set; }

        [AllowHtml]
        public string Remarks { get; set; }

        public int InvoiceStatusCode { get; set; }

        public Guid? AssessmentRoomId { get; set; }

        [Display(Name="Is accompanied by customer")]
        public bool IsAccompaniedByCustomer { get; set; }

        [Display(Name = "Is internal candidate")]
        public bool InternalCandidate { get; set; }

        [Display(Name = "Is online assessment")]
        public bool OnlineAssessment { get; set; }

        [Display(Name ="Video consent")]
        public bool Consent { get; set; }

        [Display(Name = "Archive number")]
        public int CrmCandidateInfoId { get; set; }

        public List<LanguageView> Languages { get; set; }

        public List<ReportStatusView> ReportStatuses { get; set; }

        public List<UserView> CustomerAssistants { get; set; }

        public List<AssessmentRoomView> AssessmentRooms { get; set; }

        public List<ReportDefinitionView> ReportDefinitions { get; set; }

        public List<EditProjectCategoryDetailModel> ProjectCategoryDetails { get; set; }

        public EditProjectCategoryDetailModelBase MainProjectCategoryDetail { get; set; }
        
        public AssessmentDevelopmentProjectView Project { get; set; }

        public int RetrieveOfficeId(Guid assessmentRoomId)
        {
            var assessmentRoom = AssessmentRooms.FirstOrDefault(ar => ar.Id == assessmentRoomId);
            return assessmentRoom != null
                ? assessmentRoom.OfficeId :
                1;
        }

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
                    new SelectListItem { Selected = c.Id == reportReviewerId, Text = c.FullName, Value = c.Id.ToString() })
                    .ToList());
            return customerAssistants;
        }
    }
}
