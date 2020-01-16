using System;
using Newtonsoft.Json;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice.Models
{
    public class SuperOfficeAppointmentDetail
    {
        [JsonProperty(PropertyName = "appointmentId")]
        public int? AppointmentId { get; set; }


        [JsonProperty(PropertyName = "motherId")]
        public int? MotherId { get; set; }



        #region Enriched Properties

        #endregion

        #region Computed Properties

        #endregion
    }
}
