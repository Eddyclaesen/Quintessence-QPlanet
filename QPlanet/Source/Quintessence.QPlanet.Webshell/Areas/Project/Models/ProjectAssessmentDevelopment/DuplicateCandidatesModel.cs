using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Cam;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectAssessmentDevelopment
{
    public class DuplicateCandidatesModel
    {
        public int AppointmentId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid SelectedCandidateId { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        public List<CandidateView> Candidates { get; set; }

        public string Code { get; set; }

        public int LanguageId { get; set; }

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
    }
}