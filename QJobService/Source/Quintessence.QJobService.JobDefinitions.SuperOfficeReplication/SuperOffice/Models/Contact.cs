using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.SuperOffice.Models
{
    public class Contact
    {
        [JsonProperty(PropertyName = "ContactId")]
        public int? Id { get; set; }

        public string Name { get; set; }

        #region Computed Properties

        #endregion

    }
}