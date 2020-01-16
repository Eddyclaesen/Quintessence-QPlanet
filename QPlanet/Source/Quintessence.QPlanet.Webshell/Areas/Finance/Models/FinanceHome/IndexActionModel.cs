using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QPlanet.Webshell.Areas.Finance.Models.FinanceHome
{
    public class IndexActionModel
    {
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Display(Name = "Customer assistant")]
        public Guid? CustomerAssistantId { get; set; }

        [Display(Name = "Project manager")]
        public Guid? ProjectManagerId { get; set; }

        public List<UserView> CustomerAssistants { get; set; }

        public List<UserView> ProjectManagers { get; set; }

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

        public IEnumerable<SelectListItem> CreateProjectManagerDropDownList()
        {
            return ProjectManagers.Select(projectManager => new SelectListItem
                {
                    Selected = ProjectManagerId.GetValueOrDefault() == projectManager.Id,
                    Value = projectManager.Id.ToString(),
                    Text = projectManager.FullName
                });
        }
    }
}