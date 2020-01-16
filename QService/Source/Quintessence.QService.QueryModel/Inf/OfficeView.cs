using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Inf
{
    [DataContract(IsReference = true)]
    public class OfficeView
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string ShortName { get; set; }

        [DataMember]
        public string FullName { get; set; }
    }
}
