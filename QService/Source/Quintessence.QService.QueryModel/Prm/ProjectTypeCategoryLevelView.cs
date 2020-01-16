using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectTypeCategoryLevelView : ViewEntityBase
    {
        [DataMember]
        public string Name { get; set; }
    }
}