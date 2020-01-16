using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using TLC = Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader;
using Quintessence.QJobService.JobDefinitions.TeamLeaderCommon.TeamLeader.Models;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models
{
    public class TeamLeaderMeetingDetail
    {
        private List<TeamLeaderMeetingUser> _attendingUsers = new List<TeamLeaderMeetingUser>();
        private List<TeamLeaderMeetingContact> _attendingContacts = new List<TeamLeaderMeetingContact>();

        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "creator_id")]
        public int? CreatorId { get; set; }

        [JsonProperty(PropertyName = "canceller_id")]
        public int? CancellerId { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        [JsonIgnore()]
        public List<TeamLeaderMeetingUser> AttendingUsers
        {
            get { return _attendingUsers; }            
        }

        [JsonIgnore()]
        public List<TeamLeaderMeetingContact> AttendingContacts
        {
            get { return _attendingContacts; }
        }

        public void FillAttendants(JObject jObject)
        {
            // TeamLeaders detail and list methods return customfields in different ways            
            IDictionary<string, JToken> dictionary = jObject;
            
            if (dictionary.ContainsKey("attending_internal"))
            {
                JArray meetingAttendants = dictionary["attending_internal"] as JArray;
                foreach (JObject meetingAttendant in meetingAttendants)
                {
                    TeamLeaderMeetingUser attendingUser = meetingAttendant.ToObject<TeamLeaderMeetingUser>();
                    _attendingUsers.Add(attendingUser);
                }                                                    
            }

            if (dictionary.ContainsKey("attending_external"))
            {
                JArray meetingAttendants = dictionary["attending_external"] as JArray;
                foreach (JObject meetingAttendant in meetingAttendants)
                {
                    TeamLeaderMeetingContact attendingContact = meetingAttendant.ToObject<TeamLeaderMeetingContact>();
                    _attendingContacts.Add(attendingContact);
                }
            }
        }

        #region CustomFields

        [JsonIgnore()]
        public string Afwezig { get; set; }

        [JsonIgnore()]
        public string Bedrijf { get; set; }

        [JsonIgnore()]
        public string Klant { get; set; }

        [JsonIgnore()]
        public string Intern { get; set; }

        [JsonIgnore()]
        public string Sales { get; set; }

        public void FillCustomFields(JObject jObject, IEnumerable<CustomField> customFields)
        {
            // TeamLeaders detail and list methods return customfields in different ways
            IDictionary<string, JToken> dictionary = jObject;
            bool hasCustomFieldsProperty = dictionary.ContainsKey("custom_fields");

            foreach (var customField in customFields)
            {
                switch (customField.Name)
                {
                    case TLC.Constants.CustomFieldNames.Meeting.Afwezig:
                        if (hasCustomFieldsProperty)
                        {
                            int? positieKey = jObject["custom_fields"][customField.Id.ToString()].ToObject<int?>();
                            if (positieKey.HasValue && customField.Options.ContainsKey(positieKey.Value))
                                Afwezig = customField.Options[positieKey.Value];
                        }
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            Afwezig = jObject[customField.PropertyName].ToString();
                        break;
                    case TLC.Constants.CustomFieldNames.Meeting.Bedrijf:
                        if (hasCustomFieldsProperty)
                            Bedrijf = jObject["custom_fields"][customField.Id.ToString()].ToString();
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            Bedrijf = jObject[customField.PropertyName].ToString();
                        break;
                    case TLC.Constants.CustomFieldNames.Meeting.Klant:
                        if (hasCustomFieldsProperty)
                        {
                            int? positieKey = jObject["custom_fields"][customField.Id.ToString()].ToObject<int?>();
                            if (positieKey.HasValue && customField.Options.ContainsKey(positieKey.Value))
                                Klant = customField.Options[positieKey.Value];
                        }
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            Klant = jObject[customField.PropertyName].ToString();
                        break;
                    case TLC.Constants.CustomFieldNames.Meeting.Intern:
                        if (hasCustomFieldsProperty)
                        {
                            int? positieKey = jObject["custom_fields"][customField.Id.ToString()].ToObject<int?>();
                            if (positieKey.HasValue && customField.Options.ContainsKey(positieKey.Value))
                                Intern = customField.Options[positieKey.Value];
                        }
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            Intern = jObject[customField.PropertyName].ToString();
                        break;
                    case TLC.Constants.CustomFieldNames.Meeting.Sales:
                        if (hasCustomFieldsProperty)
                        {
                            int? positieKey = jObject["custom_fields"][customField.Id.ToString()].ToObject<int?>();
                            if (positieKey.HasValue && customField.Options.ContainsKey(positieKey.Value))
                                Sales = customField.Options[positieKey.Value];
                        }
                        else if (dictionary.ContainsKey(customField.PropertyName))
                            Sales = jObject[customField.PropertyName].ToString();
                        break;
                }
            }
        }

        #endregion

        public void HtmlDecodeFields()
        {
            Title = System.Net.WebUtility.HtmlDecode(Title);
            Afwezig = System.Net.WebUtility.HtmlDecode(Afwezig);
            Bedrijf = System.Net.WebUtility.HtmlDecode(Bedrijf);
            Klant = System.Net.WebUtility.HtmlDecode(Klant);
            Intern = System.Net.WebUtility.HtmlDecode(Intern);
            Sales = System.Net.WebUtility.HtmlDecode(Sales);
            Location = System.Net.WebUtility.HtmlDecode(Location);
        }
    }
}