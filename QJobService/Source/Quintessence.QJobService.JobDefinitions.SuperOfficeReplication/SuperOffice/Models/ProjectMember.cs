using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.DataAccess;


namespace Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.SuperOffice.Models
{
    public class ProjectMember
    {
        [JsonProperty(PropertyName = "contactId")]
        public int? ContactId { get; set; }

        [JsonProperty(PropertyName = "personId")]
        public int? PersonId { get; set; }

        [JsonProperty(PropertyName = "function")]
        public string Function { get; set; }
    }
}
