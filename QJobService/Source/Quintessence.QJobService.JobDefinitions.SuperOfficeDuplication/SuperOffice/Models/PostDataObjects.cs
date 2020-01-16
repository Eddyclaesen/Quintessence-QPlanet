using Newtonsoft.Json;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice.Models
{
    public class ByProjectId
    {
        public ByProjectId(int projectId)
        {
            ProjectId = projectId;
        }

        [JsonProperty(PropertyName = "projectId")]
        public int ProjectId { get; set; }
    }
}
