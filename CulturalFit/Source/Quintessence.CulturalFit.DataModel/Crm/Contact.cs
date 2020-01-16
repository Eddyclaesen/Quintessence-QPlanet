using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Quintessence.CulturalFit.DataModel.Cfi;
using Quintessence.CulturalFit.Infra.Model;

namespace Quintessence.CulturalFit.DataModel.Crm
{
    [DataContract(IsReference = true)]
    public class Contact: IIntEntity
    {
        [DataMember]
        [Key]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Department { get; set; }
    }
}
