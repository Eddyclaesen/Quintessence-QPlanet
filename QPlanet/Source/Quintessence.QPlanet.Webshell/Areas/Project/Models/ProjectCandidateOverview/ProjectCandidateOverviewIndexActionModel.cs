using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectCandidateOverview
{
    public class ProjectCandidateOverviewIndexActionModel
    {
        public List<UserView> CustomerAssistants { get; set; }

        [Display(Name="Customer assistant")]
        public Guid? CustomerAssistantId { get; set; }

        [Display(Name = "From")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "To")]
        public DateTime? EndDate { get; set; }

        public IEnumerable<SelectListItem> CreateCustomerAssistantDropDownList()
        {
            yield return new SelectListItem
            {
                Selected = !CustomerAssistantId.HasValue,
                Value = null,
                Text = string.Empty
            };

            foreach (var customerAssistant in CustomerAssistants.OrderBy(ca => ca.FullName))
            {
                yield return new SelectListItem
                {
                    Selected = CustomerAssistantId.GetValueOrDefault() == customerAssistant.Id,
                    Value = customerAssistant.Id.ToString(),
                    Text = customerAssistant.FullName
                };
            }
        }

    }
}