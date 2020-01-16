using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Crm
{
    [DataContract]
    public class CrmEmailView
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ContactId { get; set; }

        [DataMember]
        public string ContactName { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string DirectPhone { get; set; }

        [DataMember]
        public string MobilePhone { get; set; }

        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }
    }
}