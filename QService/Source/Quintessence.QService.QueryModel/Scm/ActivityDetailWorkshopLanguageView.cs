using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Scm
{
    [DataContract(IsReference = true)]
    public class ActivityDetailWorkshopLanguageView : ViewEntityBase
    {
        [DataMember]
        public Guid ActivityDetailWorkshopId { get; set; }

        [DataMember]
        public ActivityDetailWorkshopView ActivityDetailWorkshop { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public string LanguageName { get; set; }

        [DataMember]
        public int SessionQuantity { get; set; }
    }
}