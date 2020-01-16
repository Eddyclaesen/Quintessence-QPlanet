using Newtonsoft.Json;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.SuperOffice.WebApi
{
    public class ApiResponse
    {
        [JsonProperty(PropertyName = "value")]
        public object Value { get; set; }
    }
}
