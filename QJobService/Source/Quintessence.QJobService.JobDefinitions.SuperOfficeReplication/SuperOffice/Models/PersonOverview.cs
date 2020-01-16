using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.SuperOffice.Models
{
    public class PersonOverview
    {
        [JsonProperty(PropertyName = "personId")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "personNumber")]  // = TeamleaderId
        public int? LegacyId { get; set; }

 
        #region Computed Properties


        #endregion

        #region CustomFields

        #endregion

        #region Enriched Properties

        [JsonIgnore()]
        public int? LinkedCompanyId { get; set; }

        [JsonIgnore()]
        public string LinkedCompanyName { get; set; }

        [JsonIgnore()]
        public int? CrmReplicationPersonId { get; set; }

        #endregion

        public void HtmlDecodeFields()
        {
        }
    }
}
