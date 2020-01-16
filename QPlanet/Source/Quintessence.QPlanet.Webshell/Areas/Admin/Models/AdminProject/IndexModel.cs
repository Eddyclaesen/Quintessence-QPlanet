using System.Collections.Generic;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QPlanet.Webshell.Areas.Admin.Models.AdminProject
{
    public class IndexModel
    {
        public List<ProjectRoleView> ProjectRolesForQuintessence { get; set; }
        public List<ProjectRoleView> ProjectRolesForContacts { get; set; }
    }
}