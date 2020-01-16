using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Quintessence.CulturalFit.DataModel.Cfi
{
    [DataContract(IsReference = true)]
    public class TheoremListRequestType
    {
        [DataMember]
        [Key]
        public int Id { get; set; }

        [DataMember]
        public string Type { get; set; }

        public TheoremListRequestTypeEnum Enum
        {
            get { return (TheoremListRequestTypeEnum)Id; }
            set { Id =(int)value; }
        }
    }
}
