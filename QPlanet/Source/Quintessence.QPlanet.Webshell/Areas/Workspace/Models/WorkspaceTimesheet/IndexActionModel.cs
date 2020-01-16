using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QPlanet.Webshell.Areas.Workspace.Models.WorkspaceTimesheet
{
    public class IndexActionModel
    {
        public DateTime Date { get; set; }

        public List<UserView> Users { get; set; }
        
        public Guid? UserId { get; set; }
        public Guid? ProjectManagerId { get; set; }

        public IEnumerable<SelectListItem> CreateUserDropDownList()
        {
            return Users.Select(user => new SelectListItem
                {
                    Selected = UserId == user.Id,
                    Value = user.Id.ToString(),
                    Text = user.FullName
                });
        }

        public IEnumerable<SelectListItem> CreateProjectManagerDropDownList()
        {
            return Users.Where(u => u.IsProjectManager).Select(user => new SelectListItem
                {
                    Selected = UserId == user.Id,
                    Value = user.Id.ToString(),
                    Text = user.FullName
                });
        }
    }
}