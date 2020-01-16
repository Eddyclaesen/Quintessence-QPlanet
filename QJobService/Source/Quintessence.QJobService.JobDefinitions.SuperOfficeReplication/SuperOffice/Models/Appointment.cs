using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.DataAccess;

namespace Quintessence.QJobService.JobDefinitions.SuperOfficeReplication.SuperOffice.Models
{
    public class Appointment
    {
        [JsonProperty(PropertyName = "AppointmentId")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "Associate")]
        public CompanyAssociate Associate { get; set; }

        [JsonProperty(PropertyName = "Completed")]
        public string Completed { get; set; }

        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "UserDefinedFields")]
        public UserDefinedFields UserdefinedFields { get; set; }

        [JsonProperty(PropertyName = "StartDate")]
        public DateTime? AppointmentStartDateTime { get; set; }

        [JsonProperty(PropertyName = "EndDate")]
        public DateTime? AppointmentEndDateTime { get; set; }

        [JsonProperty(PropertyName = "Project")]
        public AppointmentProject Project { get; set; }

        [JsonProperty(PropertyName = "Task")]
        public AppointmentTask Task { get; set; }

        #region Computed Properties

        [JsonIgnore()]
        public bool IsCompleted
        {
            get { return Completed.Equals("Completed", StringComparison.InvariantCultureIgnoreCase); }
        }

        [JsonIgnore()]
        public int? AssociateId
        {
            get { return (Associate != null) ? Associate.AssociateId : null; }
        }

        [JsonIgnore()]
        public string FirstName
        {
            get
            {
                return UserDefinedFields.ParseUserDefinedString(UserdefinedFields.SuperOffice1);
            }
        }

        [JsonIgnore()]
        public string LastName
        {
            get
            {
                return UserDefinedFields.ParseUserDefinedString(UserdefinedFields.SuperOffice2);
            }
        }

        [JsonIgnore()]
        public string Gender
        {
            get
            {
                return UserDefinedFields.ParseUserDefinedString(UserdefinedFields.SuperOffice3DisplayText);
            }
        }

        [JsonIgnore()]
        public int? LanguageId
        {
            get
            {
                switch (UserDefinedFields.ParseUserDefinedString(UserdefinedFields.SuperOffice4DisplayText))
                {
                    case "NL":
                        return 1;
                    case "FR":
                        return 2;
                    case "EN":
                        return 3;
                    case "DE":
                        return 4;
                    default:
                        return null;
                }
            }
        }

        [JsonIgnore()]
        public string Code
        {
            get
            {
                return UserDefinedFields.ParseUserDefinedString(UserdefinedFields.SuperOffice6);
            }
        }

        [JsonIgnore()]
        public bool IsReserved
        {
            get
            {
                return UserDefinedFields.ParseUserDefinedBool(UserdefinedFields.SuperOffice7);
            }
        }

        [JsonIgnore()]
        public string Lokatie
        {
            get
            {
                return UserDefinedFields.ParseUserDefinedString(UserdefinedFields.SuperOffice8DisplayText);
            }
        }

        [JsonIgnore()]
        public int? ProjectId
        {
            get
            {
                return (Project != null) ? Project.ProjectId : null;
            }
        }

        [JsonIgnore()]
        public string ProjectName
        {
            get
            {
                return (Project != null) ? Project.Name : String.Empty;
            }
        }

        [JsonIgnore()]
        public string TaskValue
        {
            get { return (Task != null) ? Task.Value : String.Empty; }
        }
        #endregion

        #region Enriched Properties

        [JsonIgnore()]
        public int? OfficeId { get; set; }

        [JsonIgnore()]
        public int? ProjectCrmId { get; set; }

        [JsonIgnore()]
        public int? AssociateCrmId { get; set; }

        [JsonIgnore()]
        public int? TaskCrmId { get; set; }
        #endregion

        public void HtmlDecodeFields()
        {
        }

        public bool HasUpDates(CrmReplicationContact contact)
        {
            return false;
        }
    }

    public class AppointmentAssociate
    {
        [JsonProperty(PropertyName = "AssociateId")]
        public int? AssociateId { get; set; }
    }

    public class AppointmentProject
    {
        [JsonProperty(PropertyName = "ProjectId")]
        public int? ProjectId { get; set; }

        [JsonProperty(PropertyName = "ProjectNumber")]
        public int? ProjectNumber { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
    }

    public class AppointmentTask
    {
        [JsonProperty(PropertyName = "Value")]
        public string Value { get; set; }
    }
}
