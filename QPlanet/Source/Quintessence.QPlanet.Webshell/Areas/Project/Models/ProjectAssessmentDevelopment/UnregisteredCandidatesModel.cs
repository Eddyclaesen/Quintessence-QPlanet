using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Crm;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Project.Models.ProjectAssessmentDevelopment
{
    public class UnregisteredCandidatesModel
    {
        public Guid ProjectId { get; set; }
        public List<CrmUnregisteredCandidateAppointmentView> UnregisteredCandidates { get; set; }
    }

   
}