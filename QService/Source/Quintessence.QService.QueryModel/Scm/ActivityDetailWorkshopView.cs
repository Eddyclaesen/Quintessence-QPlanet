using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Scm
{
    [DataContract(IsReference = true)]
    public class ActivityDetailWorkshopView : ActivityDetailView
    {
        [DataMember]
        public List<ActivityDetailWorkshopLanguageView> ActivityDetailWorkshopLanguages { get; set; }
    }
}