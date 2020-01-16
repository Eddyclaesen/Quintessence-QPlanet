using System;
using Newtonsoft.Json;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeDuplication.SuperOffice.Models
{
    public class SuperOfficeAppointment
    {
        [JsonProperty(PropertyName = "appointmentId")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "associateId")]
        public string Associate { get; set; }

        [JsonProperty(PropertyName = "associate/fullName")]
        public string AssociateFullName { get; set; }

        [JsonProperty(PropertyName = "associate/personId")]
        public int? AssociatePersonId { get; set; }

        [JsonProperty(PropertyName = "contactId")]
        public int? ContactId { get; set; }

        [JsonProperty(PropertyName = "personId")]
        public int? PersonId { get; set; }

        [JsonProperty(PropertyName = "projectId")]
        public int? ProjectId { get; set; }

        [JsonProperty(PropertyName = "saleId")]
        public int? SaleId { get; set; }


        [JsonProperty(PropertyName = "date")]
        public DateTime? StartDate { get; set; }

        [JsonProperty(PropertyName = "endTime")]
        public DateTime? EndDate { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "completed")]
        public bool Completed { get; set; }

        [JsonProperty(PropertyName = "appointmentUdef/SuperOffice:1")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "appointmentUdef/SuperOffice:2")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "appointmentUdef/SuperOffice:3")]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "appointmentUdef/SuperOffice:4")]
        public string Language { get; set; }

        [JsonProperty(PropertyName = "appointmentUdef/SuperOffice:5")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "appointmentUdef/SuperOffice:6")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "appointmentUdef/SuperOffice:7")]
        public bool Reservation { get; set; }

        [JsonProperty(PropertyName = "appointmentUdef/SuperOffice:8")]
        public string Location { get; set; }


        #region Enriched Properties

        public string GenderParsed
        {
            get { return UserDefinedFields.ParseUserDefinedString(Gender); }
        }

        public string LanguageParsed
        {
            get { return UserDefinedFields.ParseUserDefinedString(Language); }
        }

        public string LocationParsed
        {
            get { return UserDefinedFields.ParseUserDefinedString(Location); }
        }
        #endregion

        #region Computed Properties

        [JsonIgnore()]
        public int? DurationMinutes
        {
            get
            {
                if (StartDate.HasValue && EndDate.HasValue)
                {
                   return (int)EndDate.Value.Subtract(StartDate.Value).TotalMinutes;
                }
                return null;
            }
        }

        #endregion
    }
}
