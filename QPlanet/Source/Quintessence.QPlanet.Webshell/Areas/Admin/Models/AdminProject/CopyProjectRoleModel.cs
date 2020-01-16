using System;
using System.ComponentModel.DataAnnotations;
using Quintessence.QPlanet.Infrastructure.Web;

namespace Quintessence.QPlanet.Webshell.Areas.Admin.Models.AdminProject
{
    public class CopyProjectRoleModel
    {
        public Guid ProjectRoleId { get; set; }
        public int? ContactId { get; set; }

        public string ContactName { get; set; }
    }
}