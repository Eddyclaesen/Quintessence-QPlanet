using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class SaveProductsheetEntriesRequest
    {
        [DataMember]
        public List<CreateNewProductsheetEntryRequest> CreateProductsheetEntries { get; set; }
    }
}