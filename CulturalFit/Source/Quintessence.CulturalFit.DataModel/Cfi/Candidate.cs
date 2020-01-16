using System;
using System.Runtime.Serialization;
using Quintessence.CulturalFit.Infra.Model;

namespace Quintessence.CulturalFit.DataModel.Cfi
{
    [DataContract]
    public class Candidate : IEntity
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        [DataMember]
        public string Gender { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public Audit Audit { get; set; }
    }
}