using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectConsultancy
{
    public class ProjectTimesheetActionModel
    {
        public ConsultancyProjectView Project { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}