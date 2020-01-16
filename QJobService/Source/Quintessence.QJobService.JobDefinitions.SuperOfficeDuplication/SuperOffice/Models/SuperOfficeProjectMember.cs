using System;
using Newtonsoft.Json;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice.Models
{
    public class SuperOfficeProjectMember
    {
        [JsonProperty(PropertyName = "ProjectMemberId")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "ProjectId")]
        public int? ProjectId { get; set; }

        [JsonProperty(PropertyName = "PersonId")]
        public int? PersonId { get; set; }

        [JsonProperty(PropertyName = "ProjectMemberTypeId")]
        public int? ProjectMemberTypeId { get; set; }

        [JsonProperty(PropertyName = "ProjectMemberTypeName")]
        public string ProjectMemberTypeName { get; set; }

        #region Enriched Properties

        #endregion
    }
}
