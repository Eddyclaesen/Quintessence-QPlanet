using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Rep
{
    [DataContract(IsReference = true)]
    public class ReportTypeView
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}