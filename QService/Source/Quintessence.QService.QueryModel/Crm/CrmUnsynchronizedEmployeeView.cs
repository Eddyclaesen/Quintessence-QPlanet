using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Crm
{
    [DataContract]
    public class CrmUnsynchronizedEmployeeView
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
}