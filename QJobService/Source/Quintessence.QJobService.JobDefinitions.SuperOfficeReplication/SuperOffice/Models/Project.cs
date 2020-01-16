using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.DataAccess;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.SuperOffice.Models
{
    public class Project
    {
        [JsonProperty(PropertyName = "projectId")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "registeredDate")]
        public string RegisteredDate { get; set; }

        [JsonProperty(PropertyName = "number")]  // = TeamleaderId
        public int? LegacyId { get; set; }

        [JsonProperty(PropertyName = "Associate")] 
        public ProjectAssociate Associate { get; set; }

        [JsonProperty(PropertyName = "UserDefinedFields")]
        public UserDefinedFields UserdefinedFields { get; set; }

        [JsonProperty(PropertyName = "ProjectStatus")]
        public ProjectStatus ProjectStatus { get; set; }

        [JsonProperty(PropertyName = "ProjectMembers")]
        public List<ProjectMembers> ProjectMembers { get; set; }

        #region Computed Properties

        [JsonIgnore()]
        public int? AssociateId
        {
            get { return Associate.AssociateId; }
        }

        [JsonIgnore()]
        public int? ContactId
        {
            get
            {
                if(ProjectMembers.Any())
                    return ProjectMembers.First().ContactId;
                return null;
            }
        }

        [JsonIgnore()]
        public string BoekJaar
        {
            get
            {
                return UserDefinedFields.ParseUserDefinedString(UserdefinedFields.SuperOffice1DisplayText);
            }
        }

        [JsonIgnore()]
        public string ProjectStatusValue
        {
            get
            {
                return UserDefinedFields.ParseUserDefinedString(ProjectStatus.Value);
            }
        }

        [JsonIgnore()]
        public DateTime? BoekJaarStartDate
        {
            get
            {
                DateTime? startDate, endDate;
                DeriveBoekJaarDates(out startDate, out endDate);
                return startDate;

            }
        }

        [JsonIgnore()]
        public DateTime? BoekJaarEndDate
        {
            get
            {
                DateTime? startDate, endDate;
                DeriveBoekJaarDates(out startDate, out endDate);
                return endDate;
            }
        }
        #endregion


        #region Enriched Properties

        [JsonIgnore()]
        public int? StatusId { get; set; }

        [JsonIgnore()]
        public int? AssociateLegacyId { get; set; }

        [JsonIgnore()]
        public int? ContactLegacyId { get; set; }

        #endregion

        public void HtmlDecodeFields()
        {
            
        }

        private void DeriveBoekJaarDates(out DateTime? startDate, out DateTime? endDate)
        {
            startDate = null;
            endDate = null;
            if (!String.IsNullOrEmpty(BoekJaar) && BoekJaar.Contains(@"/"))
            {
                string[] parts = BoekJaar.Split('/');
                int startYear, endYear;
                if (parts.Length == 2 && int.TryParse(parts[0], out startYear) && int.TryParse(parts[1], out endYear))
                {
                    startDate = new DateTime(startYear + 2000, 4, 1);
                    endDate = new DateTime(endYear + 2000, 3, 31, 23, 59, 59);
                }
            }
            if (!String.IsNullOrEmpty(BoekJaar) && BoekJaar.Length == 4)
            {
                int Year;
                if (int.TryParse(BoekJaar, out Year))
                {
                    startDate = new DateTime(Year, 1, 1);
                    endDate = new DateTime(Year, 12, 31, 23, 59, 59);
                }

            }
        }

    }

    public class ProjectStatus
    {
        [JsonProperty(PropertyName = "Value")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "Id")] 
        public int? Id { get; set; }
    }

    public class ProjectAssociate
    {
        [JsonProperty(PropertyName = "AssociateId")]
        public int? AssociateId { get; set; }
    }

    public class ProjectMembers
    {
        [JsonProperty(PropertyName = "ContactId")]
        public int? ContactId { get; set; }

        [JsonProperty(PropertyName = "ContactName")]
        public string ContactName { get; set; }
    }
}
