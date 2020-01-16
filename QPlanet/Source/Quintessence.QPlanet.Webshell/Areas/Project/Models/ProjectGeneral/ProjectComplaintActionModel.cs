using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QPlanet.ViewModel.Prm;
using Quintessence.QService.DataModel.Inf;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Inf;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectGeneral
{
    public class ProjectComplaintActionModel
    {
        public List<ComplaintTypeView> Complaint { get; set; }

        public int CrmProjectId { get; set; }
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Subject")]
        public string Subject { get; set; }
        [Required]
        [Display(Name = "Submitter")]
        public string SubmitterName { get; set; }

        [Required]
        public Guid SubmitterId { get; set; }

        [Required]
        [Display(Name = "Date")]
        public DateTime ComplaintDate { get; set; }

        [Display(Name = "Details")]
        public string ComplaintDetails { get; set; }

        [Display(Name = "Follow-up")]
        public string FollowUp { get; set; }
        [Required]
        [Display(Name = "Status")]
        public int ComplaintStatusTypeId { get; set; }
        [Required]
        [Display(Name = "Severity")]
        public int ComplaintSeverityTypeId { get; set; }

        [Display(Name = "Complaint")]
        public int ComplaintTypeId { get; set; }

        public List<SelectListItem> CreateComplaintTypeSelectListItems(int selectedProjectComplaintId)
        {
            return Complaint.Select(eft => new SelectListItem { Selected = eft.Id == selectedProjectComplaintId, Text = eft.Name, Value = eft.Id.ToString() }).ToList();
        }

        public List<SelectListItem> CreateComplaintStatusTypeSelectListItems(int ComplaintStatusType)
        {
            var x =  Enum.GetValues(typeof(ComplaintStatusType)).OfType<ComplaintStatusType>().Select(g =>
                                                                                            new SelectListItem
                                                                                            {
                                                                                                Value = Convert.ToString((int)g),
                                                                                                Text = EnumMemberNameAttribute.GetName(g),
                                                                                                Selected = g.ToString() == ComplaintStatusType.ToString()
                                                                                            }).ToList();
            return x;
        }

        public List<SelectListItem> CreateComplaintSeverityTypeSelectListItems(int ComplaintSeverityType)
        {
            return Enum.GetValues(typeof(ComplaintSeverityType)).OfType<ComplaintSeverityType>().Select(g =>
                                                                                            new SelectListItem
                                                                                            {
                                                                                                Value = Convert.ToString((int)g),
                                                                                                Text = EnumMemberNameAttribute.GetName(g),
                                                                                                Selected = g.ToString() == ComplaintSeverityType.ToString()
                                                                                            }).ToList();
        }


    }
}