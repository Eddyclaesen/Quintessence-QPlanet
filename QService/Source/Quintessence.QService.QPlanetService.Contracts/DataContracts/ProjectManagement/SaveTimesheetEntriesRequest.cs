using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class SaveTimesheetEntriesRequest
    {
        [DataMember]
        public List<UpdateTimesheetEntryRequest> UpdateTimesheetEntries { get; set; }
        
        [DataMember]
        public List<CreateNewTimesheetEntryRequest> CreateTimesheetEntries { get; set; }
    }
}
