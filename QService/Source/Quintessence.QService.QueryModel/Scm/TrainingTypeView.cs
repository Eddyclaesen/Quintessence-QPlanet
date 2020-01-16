using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Scm
{
    [DataContract(IsReference = true)]
    public class TrainingTypeView : ViewEntityBase
    {
        [DataMember]
        public string Name { get; set; }
    }
}
