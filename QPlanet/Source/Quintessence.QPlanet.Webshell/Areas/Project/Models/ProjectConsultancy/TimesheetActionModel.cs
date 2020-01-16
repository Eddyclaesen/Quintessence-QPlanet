using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectConsultancy
{
    public class TimesheetActionModel
    {
        public ConsultancyProjectView Project { get; set; }

        public List<SelectListItem> Months
        {
            get
            {
                var months = new List<SelectListItem>();

                for (int i = 1; i < 13; i++)
                {
                    var monthDate = new DateTime(2012, i, 1);
                    months.Add(
                        new SelectListItem
                            {
                                Value = i.ToString(CultureInfo.InvariantCulture),
                                Text = monthDate.ToString("MMMM"),
                                Selected = DateTime.Now.Month == i
                            }
                        );
                }
                return months;
            }
        }

        public IEnumerable<SelectListItem> Years
        {
            get
            {
                for (int i = DateTime.Now.Year - 25; i < DateTime.Now.Year + 1; i++)
                {
                    yield return new SelectListItem
                        {
                            Value = i.ToString(CultureInfo.InvariantCulture),
                            Text = i.ToString(CultureInfo.InvariantCulture),
                            Selected = DateTime.Now.Year == i
                        };
                }
            }
        }

        public bool IsCurrentUserProjectManager { get; set; }

        public bool IsCurrentUserCustomerAssistant { get; set; }
    }
}