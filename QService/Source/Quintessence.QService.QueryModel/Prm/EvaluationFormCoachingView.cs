using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class EvaluationFormCoachingView : EvaluationFormView
    {
        [DataMember]
        public string Question01 { get; set; }

        [DataMember]
        public string Question02 { get; set; }

        [DataMember]
        public bool? Question03 { get; set; }

        [DataMember]
        public bool? Question04A { get; set; }

        [DataMember]
        public string Question04A_41A_411 { get; set; }

        [DataMember]
        public string Question04A_41A_412 { get; set; }

        [DataMember]
        public string Question04A_41A_413 { get; set; }

        [DataMember]
        public string Question04A_41B_411 { get; set; }

        [DataMember]
        public string Question04A_41B_412 { get; set; }

        [DataMember]
        public string Question04B { get; set; }

        [DataMember]
        public bool? Question05 { get; set; }

        [DataMember]
        public bool? Question05_5A { get; set; }

        [DataMember]
        public string Question05_5A_51A_511 { get; set; }

        [DataMember]
        public string Question05_5A_51A_512 { get; set; }

        [DataMember]
        public string Question05_5A_51A_513 { get; set; }

        [DataMember]
        public string Question05_5A_51B_511 { get; set; }

        [DataMember]
        public string Question05_5A_51B_512 { get; set; }

        [DataMember]
        public string Question05_5B { get; set; }

        [DataMember]
        public string Question06 { get; set; }

        [DataMember]
        public string Question07 { get; set; }

        [DataMember]
        public string Question08 { get; set; }

        [DataMember]
        public string Question09 { get; set; }

        [DataMember]
        public bool? Question10 { get; set; }

        [DataMember]
        public string Question10_10A { get; set; }

        [DataMember]
        public string Question10_10B { get; set; }

        [DataMember]
        public string Question11 { get; set; }

        [DataMember]
        public int? Question12 { get; set; }

        [DataMember]
        public int? Question13 { get; set; }

        [DataMember]
        public string Question14 { get; set; }

        [DataMember]
        public string Question15 { get; set; }

        [DataMember]
        public bool? Question16 { get; set; }

        [DataMember]
        public string Question16_16A { get; set; }

        [DataMember]
        public string Question16_16B { get; set; }
    }
}