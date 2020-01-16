using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Sof
{
    [DataContract(IsReference = true)]
    public class CrmContactView
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Department { get; set; }

        [DataMember]
        public int? AssociateId { get; set; }

        [DataMember]
        public int? AccountManagerId { get; set; }

        [DataMember]
        public int? CustomerAssistantId { get; set; }

        [DataMember]
        public CrmAssociateView CustomerAssistant { get; set; }

        [DataMember]
        public CrmAssociateView AccountManager { get; set; }

        public string FullName
        {
            get
            {
                return string.IsNullOrWhiteSpace(Department)
                    ? Name
                    : string.Format("{0} ({1})", Name, Department);
            }
        }

        public override bool Equals(object obj)
        {
            CrmContactView contactObj = obj as CrmContactView;
            if (contactObj == null)
                return false;
            else
                return Id.Equals(contactObj.Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
