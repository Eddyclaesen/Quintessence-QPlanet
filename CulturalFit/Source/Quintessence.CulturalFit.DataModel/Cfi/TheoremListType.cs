using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Quintessence.CulturalFit.DataModel.Cfi
{
    [DataContract(IsReference = true)]
    public class TheoremListType
    {
        [DataMember]
        [Key]
        public int Id { get; set; }

        [DataMember]
        public string Type { get; set; }

        public TheoremListTypeEnum Enum
        {
            get { return (TheoremListTypeEnum)Id; }
            set { Id = (int)value; }
        }
    }
}
