using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Sec
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(EmployeeView))]
    public class UserView : ViewEntityBase
    {
        [DataMember]
        public int AssociateId { get; set; }

        [DataMember]
        public Guid? RoleId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public string LanguageCode { get; set; }

        [DataMember]
        public string LanguageName { get; set; }

        [DataMember]
        public bool IsEmployee { get; set; }

        [DataMember]
        public bool IsLocked{ get; set; }

        [DataMember]
        public string RoleName { get; set; }

        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        public string FormalFullName
        {
            get { return string.Format("{0}, {1}", LastName, FirstName); }
        }

        [DataMember]
        public string RoleCode { get; set; }

        [DataMember]
        public bool IsProjectManager { get; set; }
    }
}
