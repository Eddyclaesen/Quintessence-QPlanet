using System;
using Newtonsoft.Json;

using TLC = Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models
{
    public class TeamLeaderTimeTracking
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "project_id")]
        public int? ProjectId { get; set; }

        [JsonProperty(PropertyName = "project_title")]
        public string ProjectTitle { get; set; }

        [JsonProperty(PropertyName = "user_id")]
        public int? UserId { get; set; }

        [JsonProperty(PropertyName = "employee_name")]
        public string EmployeeName { get; set; }

        [JsonProperty(PropertyName = "date")]
        public double? Date { get; set; }

        [JsonProperty(PropertyName = "duration")]
        public int? Duration { get; set; }

        [JsonProperty(PropertyName = "contact_id")]
        public int? ContactId { get; set; }

        [JsonProperty(PropertyName = "contact_name")]
        public string ContactName { get; set; }

        [JsonProperty(PropertyName = "company_id")]
        public int? CompanyId { get; set; }

        [JsonProperty(PropertyName = "company_name")]
        public string CompanyName { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Category { get; set; }

        [JsonProperty(PropertyName = "related_to")]
        public RelatedTo IsRelatedTo { get; set; }

        #region Computed Properties

        [JsonIgnore()]
        public DateTime? StartDate
        {
            get
            {
                return TLC.DateTimeHelper.UnixTimeStampToDateTime(Date);
            }
        }

        [JsonIgnore()]
        public DateTime? EndDate
        {
            get
            {
                if (Duration.HasValue)
                {
                    DateTime? startDate = TLC.DateTimeHelper.UnixTimeStampToDateTime(Date);
                    if (startDate.HasValue)
                        return startDate.Value.AddMinutes(Duration.Value);
                }
                return null;
            }
        }
        #endregion

        public void HtmlDecodeFields()
        {
            ProjectTitle = System.Net.WebUtility.HtmlDecode(ProjectTitle);
            EmployeeName = System.Net.WebUtility.HtmlDecode(EmployeeName);
            ContactName = System.Net.WebUtility.HtmlDecode(ContactName);
            CompanyName = System.Net.WebUtility.HtmlDecode(CompanyName);
            Description = System.Net.WebUtility.HtmlDecode(Description);
        }
    }
}
