using System;
using Newtonsoft.Json;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice.Models
{
    public class SuperOfficeProject
    {
        [JsonProperty(PropertyName = "PrimaryKey")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "projectAssociate/fullName")]
        public string ProjectAssociateFullName { get; set; }

        [JsonProperty(PropertyName = "projectAssociate/personId")]
        public int? ProjectAssociatePersonId { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "registeredDate")]
        public DateTime? RegisteredDate { get; set; }

        [JsonProperty(PropertyName = "endDate")]
        public DateTime? EndDate { get; set; }

        [JsonProperty(PropertyName = "projectUdef/SuperOffice:1")]
        public string BookYear { get; set; }

        [JsonProperty(PropertyName = "nextMileStone")]
        public DateTime? NextMileStone { get; set; }

        [JsonProperty(PropertyName = "completed")]
        public bool Completed { get; set; }

        [JsonProperty(PropertyName = "updatedDate")]
        public DateTime? UpdatedDate { get; set; }

        [JsonProperty(PropertyName = "updatedBy")]
        public string UpdatedBy { get; set; }

        #region Enriched Properties

        [JsonIgnore()]

        public string TypeParsed
        {
            get
            {
                return UserDefinedFields.ParseUserDefinedString(Type);
            }
        }

        public string StatusParsed
        {
            get
            {
                return UserDefinedFields.ParseUserDefinedString(Status);
            }
        }

        [JsonIgnore()]
        public string BookYearParsed
        {
            get
            {
                return UserDefinedFields.ParseUserDefinedString(BookYear);
            }
        }
        #endregion
    }
}
