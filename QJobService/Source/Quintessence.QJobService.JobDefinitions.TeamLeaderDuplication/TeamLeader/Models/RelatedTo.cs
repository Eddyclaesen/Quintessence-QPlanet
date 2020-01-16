using Newtonsoft.Json;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models
{
    public class RelatedTo
    {
        [JsonProperty(PropertyName = "object_type")]
        public string ObjectType { get; set; }

        [JsonProperty(PropertyName = "object_id")]
        public int? ObjectId { get; set; }

        [JsonIgnore()]
        public string TranslatedObjectType
        {
            get
            {                
                switch(ObjectType)
                {
                    case "todo":
                        return "task";
                }
                return ObjectType;
            }
        }
    }
}
