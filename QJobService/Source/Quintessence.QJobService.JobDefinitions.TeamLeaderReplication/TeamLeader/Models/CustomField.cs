using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderReplication.TeamLeader.Models
{
    public class CustomField
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "for")]
        public string TargetObject { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string ObjectType { get; set; }

        [JsonProperty(PropertyName = "group")]
        public string Group { get; set; }

        [JsonIgnore()]
        public string PropertyName
        {
            get { return Id.HasValue ? String.Format("cf_value_{0}", Id.Value) : null; }
        }

        public Dictionary<int,string> Options { get; set; }

    }
}


