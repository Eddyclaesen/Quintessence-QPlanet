using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Quintessence.CulturalFit.Service.Contracts.DataContracts
{
    [DataContract(IsReference = true)]
    public class ContactRequest
    {
        [DataMember]
        public int ContactId { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public int Type { get; set; }

        [DataMember]
        public string ContactName { get; set; }

        [DataMember]
        public Guid? ContactPersonId { get; set; }

        [DataMember]
        public string ContactPersonFirstName { get; set; }

        [DataMember]
        public string ContactPersonLastName { get; set; }

        [DataMember]
        public string ContactPersonEmail { get; set; }

        [DataMember]
        public int ContactPersonGender { get; set; }

        [DataMember]
        public DateTime Deadline { get; set; }

        [DataMember]
        public Guid TemplateId { get; set; }
    }
}
