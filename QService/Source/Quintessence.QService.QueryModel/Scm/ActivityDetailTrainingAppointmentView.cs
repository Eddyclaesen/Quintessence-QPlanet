using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Scm
{
    [DataContract(IsReference = true)]
    public class ActivityDetailTrainingAppointmentView
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int AssociateId { get; set; }

        [DataMember]
        public int OfficeId { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public string UserFirstName { get; set; }

        [DataMember]
        public string UserLastName { get; set; }

        [DataMember]
        public string OfficeFullName { get; set; }

        [DataMember]
        public string OfficeShortName { get; set; }

        [DataMember]
        public string LanguageName { get; set; }

        public string UserFullName
        {
            get { return string.Format("{0} {1}", UserFirstName, UserLastName); }
        }
    }
}