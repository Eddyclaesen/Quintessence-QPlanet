using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectCandidateReportingOverview
{
    public class ProjectCandidateReportingOverviewIndexActionModel
    {
        public List<UserView> CustomerAssistants { get; set; }

        [Display(Name = "Customer assistant")]
        public Guid? CustomerAssistantId { get; set; }

        [Display(Name = "Date")]
        public DateTime? StartDate { get; set; }

        public IEnumerable<SelectListItem> CreateCustomerAssistantDropDownList()
        {
            yield return new SelectListItem
                {
                    Selected = !CustomerAssistantId.HasValue,
                    Value = null,
                    Text = string.Empty
                };

            foreach (var customerAssistant in CustomerAssistants)
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