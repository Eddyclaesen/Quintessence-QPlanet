using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Scm
{
    [DataContract(IsReference = true)]
    public class ActivityDetailTrainingTypeView : ViewEntityBase
    {
        [DataMember]
        public Guid? ActivityDetailTrainingId { get; set; }

        [DataMember]
        public ActivityDetailTrainingView ActivityDetailTraining { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
