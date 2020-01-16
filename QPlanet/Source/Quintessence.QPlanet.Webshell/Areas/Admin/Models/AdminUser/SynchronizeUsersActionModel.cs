using System.Collections.Generic;
using Quintessence.QPlanet.ViewModel.Crm;

namespace Quintessence.QPlanet.Webshell.Areas.Admin.Models.AdminUser
{
    public class SynchronizeUsersActionModel
    {
        public List<UnsynchronizedEmployeeModel> UnsynchronizedEmployees { get; set; }
    }
}