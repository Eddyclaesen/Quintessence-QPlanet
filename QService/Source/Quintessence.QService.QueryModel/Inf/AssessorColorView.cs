using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Inf
{
    [DataContract(IsReference = true)]
    public class AssessorColorView
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Color { get; set; }
    }
}