using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.DataAccess;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.SuperOffice.Models
{
    public class Company
    {
        [JsonProperty(PropertyName = "contactid")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "Associate")]
        public CompanyAssociate Associate { get; set; }

        [JsonProperty(PropertyName = "number")]  // = TeamLeaderId, stored in superOffice as number
        public int? LegacyId { get; set; }

        [JsonProperty(PropertyName = "Department")] 
        public string Department { get; set; }

        [JsonProperty(PropertyName = "UserDefinedFields")]
        public UserDefinedFields UserdefinedFields { get; set; }

        #region Computed Properties

        [JsonIgnore()]
        public int? AssociateId
        {
            get { return Associate.AssociateId; }
        }

        [JsonIgnore()]
        public int? AccountManagerId
        {
            get
            {
                return UserDefinedFields.ParseUserDefinedInt(UserdefinedFields.SuperOffice1);
            }
        }

        [JsonIgnore()]
        public int? AssistentId
        {
            get
            {
                return UserDefinedFields.ParseUserDefinedInt(UserdefinedFields.SuperOffice2);
            }
        }

        #endregion

        #region Enriched Properties

        [JsonIgnore()]
        public int? AssociateCrmId { get; set; }

        [JsonIgnore()]
        public int? AccountManagerCrmId { get; set; }

        [JsonIgnore()]
        public int? AssistentCrmId { get; set; }

        #endregion

        public void HtmlDecodeFields()
        {
        }
    }

    public class CompanyAssociate
    {
        [JsonProperty(PropertyName = "AssociateId")]
        public int? AssociateId { get; set; }
    }
}
