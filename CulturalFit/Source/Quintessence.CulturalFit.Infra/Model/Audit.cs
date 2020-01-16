using System;
using System.Runtime.Serialization;

namespace Quintessence.CulturalFit.Infra.Model
{
    [DataContract]
    public class Audit
    {
        [DataMember]
        public string CreatedBy { get; set; }

        [DataMember]
        public DateTime CreatedOn { get; set; }

        [DataMember]
        public string ModifiedBy { get; set; }

        [DataMember]
        public DateTime? ModifiedOn { get; set; }

        [DataMember]
        public string DeletedBy { get; set; }

        [DataMember]
        public DateTime? DeletedOn { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }

        [DataMember]
        public Guid VersionId { get; set; }
    }
}
