using Newtonsoft.Json;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice.WebApi
{
    public class ApiResponse
    {
        [JsonProperty(PropertyName = "value")]
        public object Value { get; set; }
    }
}
