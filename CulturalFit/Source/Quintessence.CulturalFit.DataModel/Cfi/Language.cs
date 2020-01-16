using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Quintessence.CulturalFit.Infra.Model;

namespace Quintessence.CulturalFit.DataModel.Cfi
{
    [DataContract(IsReference = true)]
    public class Language: IIntEntity
    {
        [DataMember]
        [Key]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Code { get; set; }
    }
}
