using System.Runtime.Serialization;

namespace Quintessence.QService.DataModel.Inf
{
    [DataContract]
    public class Office
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string ShortName { get; set; }

        [DataMember]
        public string FullName { get; set; }
    }
}
