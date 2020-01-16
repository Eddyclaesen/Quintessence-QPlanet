using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Inf
{
    [DataContract(IsReference = true)]
    public class LanguageView
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Code { get; set; }
    }
}
