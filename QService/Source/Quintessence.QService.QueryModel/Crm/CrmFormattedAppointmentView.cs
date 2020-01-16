using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Crm
{
    [DataContract]
    public class CrmFormattedAppointmentView
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public DateTime AppointmentDate { get; set; }

        [DataMember]
        public int OfficeId { get; set; }
    }
}