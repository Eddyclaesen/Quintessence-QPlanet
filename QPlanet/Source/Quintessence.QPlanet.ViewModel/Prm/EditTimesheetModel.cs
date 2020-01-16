using System.Collections.Generic;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QPlanet.ViewModel.Prm
{
    public class EditTimesheetModel
    {
        public UserView User { get; set; }

        public List<EditTimesheetEntryModel> Entries { get; set; }
    }
}