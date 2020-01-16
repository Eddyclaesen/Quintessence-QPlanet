using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Cam
{
    [DataContract]
    public class ProgramComponentSpecialView : ViewEntityBase
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Execution { get; set; }

        [DataMember]
        public bool IsWithLeadAssessor { get; set; }
    }
}